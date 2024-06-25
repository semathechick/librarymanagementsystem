import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Register } from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpClient : HttpClient) { }
  
  apiUrl: string = "http://localhost:60805/api/Members";

  Rgstr(registerModel:Register):Observable<Register>{
    return this.httpClient.post<Register>(this.apiUrl,registerModel);
  }
}
