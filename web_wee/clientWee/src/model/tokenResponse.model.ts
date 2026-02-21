export class TokenResponse
{
    accessToken: string;
    refreshToken: string;
    expiresAt: Date;

     constructor() {                
    this.accessToken = '' ;
    this.refreshToken = '';
    this.expiresAt = new Date;
  }
}