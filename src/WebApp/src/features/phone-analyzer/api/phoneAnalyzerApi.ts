import { apiClient } from "@/shared/api/client";
import type { PhoneAnalysisResult } from "@/shared/api/Api";

export async function analyzePhoneNumber(
  request: string,
): Promise<PhoneAnalysisResult[]> {
  const { data } = await apiClient.api.phoneAnalysisCreate(request);

  return data;
}
