import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  setToken(token: string) {
    localStorage.setItem('Token', token);
  }

  getToken() {
    return localStorage.getItem('Token');
  }

  hasToken(): boolean {
    return this.getToken() != null;
  }
  removeToken(){
    localStorage.removeItem('Token');
  }

}
