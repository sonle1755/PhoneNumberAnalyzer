let accessToken: string | null = null;
let accessTokenExpiresAt: number | null = null;

export function setAccessToken(token: string, expiresIn: number): void {
  accessToken = token;
  accessTokenExpiresAt = Date.now() + expiresIn * 1000;
}

export function getAccessToken(): string | null {
  return accessToken;
}

export function isAccessTokenExpired(): boolean {
  if (accessTokenExpiresAt === null) {
    return true;
  }

  return Date.now() >= accessTokenExpiresAt;
}

export function clearAccessToken(): void {
  accessToken = null;
  accessTokenExpiresAt = null;
}
