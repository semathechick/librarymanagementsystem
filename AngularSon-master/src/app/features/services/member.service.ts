import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../models/member';
import { ResponseModel } from '../models/responseModel';
import { Observable } from 'rxjs';
import { GetAllBook } from '../models/getAllBook';
import { Response } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private httpClient:HttpClient) { }
  apiUrl:string = "http://localhost:60805/api/Members";

  getAll():Observable<ResponseModel<Member>>{
    return this.httpClient.get<ResponseModel<Member>>(
      this.apiUrl+'?PageIndex=0&PageSize=10'
    );}

    getById(id:number):Observable<Response<Member>>{
      return this.httpClient.get<Response<Member>>(this.apiUrl+'/'+id)
    }

    
  
}
