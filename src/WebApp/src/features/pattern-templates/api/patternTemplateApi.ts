import {
  getApiPatternTemplate,
  getApiPatternTemplateById,
  postApiPatternTemplate,
  putApiPatternTemplateById,
} from "@/client";
import type {
  PatternTemplateDetail,
  PatternTemplateCreateCommand,
  PatternTemplateUpdateCommand,
} from "@/client";

export async function getPatternTemplates(): Promise<PatternTemplateDetail[]> {
  const { data } = await getApiPatternTemplate();
  return data;
}

export async function getPatternTemplateById(
  id: string,
): Promise<PatternTemplateDetail> {
  const { data } = await getApiPatternTemplateById({ path: { id: id } });
  return data;
}

export async function createPatternTemplate(
  command: PatternTemplateCreateCommand,
): Promise<PatternTemplateDetail> {
  const { data } = await postApiPatternTemplate({ body: command });
  return data;
}

export async function updatePatternTemplate(
  id: string,
  command: PatternTemplateUpdateCommand,
): Promise<void> {
  await putApiPatternTemplateById({ path: { id: id }, body: command });
}
