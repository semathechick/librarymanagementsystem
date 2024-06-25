import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private httpClient:HttpClient) { }
  apiUrl:string = "http://localhost:60805/api/Members";
  editMemberProfile(member:Member):Observable<any>{
    return this.httpClient.put<any>(this.apiUrl,member)
  }
}
