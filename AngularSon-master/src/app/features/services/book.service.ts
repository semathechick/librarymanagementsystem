import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Book } from '../models/book';
import { ResponseModel } from '../models/responseModel';

import { Response } from '../models/response';
import { GetAllBook } from '../models/getAllBook';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class BookService {

 // loadedBook: GetAllBook | null=null;
 selectedBook: any; // Seçilen kitabı saklamak için değişken

  constructor(private httpClient:HttpClient) { }
  apiUrl:string = "http://localhost:60805/api/Books";
  
  getAll():Observable<ResponseModel<GetAllBook>>{
      return this.httpClient.get<ResponseModel<GetAllBook>>(
        this.apiUrl+'?PageIndex=0&PageSize=100'
      );
  }

  getById(id:number):Observable<Response<Book>>{
    return this.httpClient.get<Response<Book>>(this.apiUrl+'/'+id)
  }
  
  add(book:Book):Observable<Book>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.post<Book>(this.apiUrl,book,{headers:headers})
  }

  editBook(book:Book):Observable<any>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.put<any>(this.apiUrl,book,{headers:headers})
  }
  deleteBook(bookId:number){
    return this.httpClient.delete(this.apiUrl+'/'+bookId);
  }
  getBooksByCategoryId(categoryId:number):Observable<Response<Book>>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.get<Response<Book>>(this.apiUrl+'/getbooksbycategoryid?PageIndex=0&PageSize=20&categoryId='+categoryId,{headers:headers})
  }

  getBooksByAuthorId(authorId:number):Observable<Response<Book>>{
    const token = localStorage.getItem('Token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.httpClient.get<Response<Book>>(this.apiUrl+'/getbooksbyauthorid?PageIndex=0&PageSize=20&authorId='+authorId,{headers:headers})
  
  }


}
