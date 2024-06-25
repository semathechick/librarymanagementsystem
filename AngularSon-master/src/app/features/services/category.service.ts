import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/responseModel';
import { Category } from '../models/Category';
import { Response } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient:HttpClient) { }
 
  selectedCategory:any;
  apiUrl="http://localhost:60805/api/Categories";

  getAll():Observable<ResponseModel<Category>>{
      return this.httpClient.get<ResponseModel<Category>>(
        this.apiUrl+'?PageIndex=0&PageSize=100'
      );
  }

  getById(id:number):Observable<Response<Category>>{
    return this.httpClient.get<Response<Category>>(this.apiUrl+'/'+id)
  }

  add(category:Category):Observable<any>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.post<any>(this.apiUrl,category,{headers:headers})
  }
  editCategory(category:Category):Observable<any>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.put<any>(this.apiUrl,category,{headers:headers})
  }

  deleteCategory(categoryId:number){
    return this.httpClient.delete(this.apiUrl+'/'+categoryId);
  }
}
