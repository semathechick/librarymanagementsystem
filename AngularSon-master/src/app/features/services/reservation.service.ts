import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Reservation } from '../models/Reservation';
import { ResponseModel } from '../models/responseModel';
import { Response } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  apiUrl="http://localhost:60805/api/Rezervations";
  constructor(private http:HttpClient) { }

  createReservation(reservation: Reservation): Observable<Reservation> {
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<Reservation>(this.apiUrl, reservation, { headers: headers });
  }

  getReservations(memberId: string): Observable<Response<Reservation>> {
    const token = localStorage.getItem('Token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Response<Reservation>>(`${this.apiUrl}/getreservationsbymemberid?PageIndex=0&PageSize=10&memberId=${memberId}`, { headers: headers });

  }
}
