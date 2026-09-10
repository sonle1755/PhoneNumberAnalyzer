using Moq;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Business.Services;
using PhoneNumberAnalyzer.Business.Specifications;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Business.Tests.Services;

public class PatternTemplateEvaluationServiceTests
{
    private readonly Mock<IPatternTemplateRepository> _repositoryMock = new();
    private readonly Mock<IPatternSpecificationCache> _specCacheMock = new();
    private readonly PatternTemplateEvaluationService _service;

    public PatternTemplateEvaluationServiceTests()
    {
        _service = new PatternTemplateEvaluationService(_repositoryMock.Object, _specCacheMock.Object);
    }

    private static PatternTemplate CreateTemplate(Guid id, string name) => new()
    {
        Id = id,
        Name = name,
        RuleGroups = [new PatternRuleGroup()],
    };

    private sealed class StubSpecification : ISpecification
    {
        private readonly bool _result;
        public StubSpecification(bool result) => _result = result;
        public bool IsSatisfiedBy(string digits) => _result;
    }

    [Fact]
    public async Task EvaluateAsync_MatchingTemplate_ReturnsIt()
    {
        var template = CreateTemplate(Guid.NewGuid(), "Lucky Number");

        _repositoryMock
            .Setup(r => r.GetVisibleToUserAsync(It.IsAny<int>()))
            .ReturnsAsync([template]);

        _specCacheMock
            .Setup(c => c.GetOrBuild(template))
            .Returns(new StubSpecification(true));

        var result = await _service.EvaluateAsync("0912345678", userId: 1);

        Assert.Single(result);
        Assert.Equal(template.Id, result.First().TemplateId);
    }

    [Fact]
    public async Task EvaluateAsync_NonMatchingTemplate_ExcludesIt()
    {
        var template = CreateTemplate(Guid.NewGuid(), "Lucky Number");

        _repositoryMock
            .Setup(r => r.GetVisibleToUserAsync(It.IsAny<int>()))
            .ReturnsAsync([template]);

        _specCacheMock
            .Setup(c => c.GetOrBuild(template))
            .Returns(new StubSpecification(false));

        var result = await _service.EvaluateAsync("0912345678", userId: 1);

        Assert.Empty(result);
    }

    [Fact]
    public async Task EvaluateAsync_NullUserId_UsesPublicTemplatesOnly()
    {
        _repositoryMock
            .Setup(r => r.GetPublicAsync())
            .ReturnsAsync([]);

        await _service.EvaluateAsync("0912345678", userId: null);

        _repositoryMock.Verify(r => r.GetPublicAsync(), Times.Once);
        _repositoryMock.Verify(r => r.GetVisibleToUserAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task EvaluateAsync_MultipleMatches_ReturnsAllWithCorrectWeights()
    {
        var templateA = CreateTemplate(Guid.NewGuid(), "Template A");
        var templateB = CreateTemplate(Guid.NewGuid(), "Template B");

        _repositoryMock
            .Setup(r => r.GetVisibleToUserAsync(It.IsAny<int>()))
            .ReturnsAsync([templateA, templateB]);

        _specCacheMock.Setup(c => c.GetOrBuild(templateA)).Returns(new StubSpecification(true));
        _specCacheMock.Setup(c => c.GetOrBuild(templateB)).Returns(new StubSpecification(true));

        var result = await _service.EvaluateAsync("0912345678", userId: 1);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, m => m.TemplateId == templateA.Id);
        Assert.Contains(result, m => m.TemplateId == templateB.Id);
    }
}
