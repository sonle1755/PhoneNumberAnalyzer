import { postApiPhoneAnalysis } from "@/client";
import type { PhoneAnalysisResult } from "@/client";

export async function analyzePhoneNumber(
  request: string,
): Promise<PhoneAnalysisResult[]> {
  const { data } = await postApiPhoneAnalysis({ body: request });

  return data;
}
