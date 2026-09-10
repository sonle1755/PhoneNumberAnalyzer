import { apiClient } from "../../../shared/api/client";
import type {
  PatternTemplateDetail,
  PatternTemplateCreateCommand,
  PatternTemplateUpdateCommand,
} from "@/shared/api/Api";

export async function getPatternTemplates(): Promise<PatternTemplateDetail[]> {
  const { data } = await apiClient.api.patternTemplateList();
  return data;
}

export async function getPatternTemplateById(
  id: string,
): Promise<PatternTemplateDetail> {
  const { data } = await apiClient.api.patternTemplateDetail(id);
  return data;
}

export async function createPatternTemplate(
  command: PatternTemplateCreateCommand,
): Promise<PatternTemplateDetail> {
  const { data } = await apiClient.api.patternTemplateCreate(command);
  return data;
}

export async function updatePatternTemplate(
  id: string,
  command: PatternTemplateUpdateCommand,
): Promise<void> {
  await apiClient.api.patternTemplateUpdate(id, command);
}

export async function disablePatternTemplate(id: string): Promise<void> {
  await apiClient.api.patternTemplateDisableUpdate(id);
}

export async function enablePatternTemplate(id: string): Promise<void> {
  await apiClient.api.patternTemplateEnableUpdate(id);
}
