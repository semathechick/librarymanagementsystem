export interface AccessToken {
    token: string;
    expirationDate: string; 
    email:string;
  }
  export interface LoginResponse {
    accessToken: AccessToken;
    requiredAuthenticatorType: null | string; 
    email:string;
  }
  
