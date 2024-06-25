import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/responseModel';
import { Response } from '../models/response';
import { Announcement } from '../models/Announcement';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  constructor(private httpClient:HttpClient) { }

  apiUrl="http://localhost:60805/api/Announcements";

  getAll():Observable<ResponseModel<Announcement>>{
      return this.httpClient.get<ResponseModel<Announcement>>(
        this.apiUrl+'?PageIndex=0&PageSize=100'
      );
  }

  getById(id:number):Observable<Response<Announcement>>{
    return this.httpClient.get<Response<Announcement>>(this.apiUrl+'/'+id)
  }

  add(announcement:Announcement):Observable<any>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.post<any>(this.apiUrl,announcement,{headers:headers})
  }
  editAnnouncement(announcement: Announcement):Observable<any>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.put<any>(this.apiUrl,announcement,{headers:headers})
  }

  deleteAnnouncement(announcementId:number){
    return this.httpClient.delete(this.apiUrl+'/'+announcementId);
  }

}