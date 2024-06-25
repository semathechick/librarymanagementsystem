import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResponse } from '../models/AccessToken';


@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private httpClient: HttpClient) { }
  apiUrl: string = "http://localhost:60805/api/Auth/Login";

  Login(email: string, password: string, authenticatorCode: string): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(this.apiUrl, {
      email: email,
      password: password,
      authenticatorCode: authenticatorCode
    });
  }
}