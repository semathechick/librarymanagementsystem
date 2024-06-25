import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDo } from '../models/ToDo';

@Injectable({
  providedIn: 'root'
})
export class ToDoService {

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<ToDo[]> {
    return this.httpClient.get<ToDo[]>(
      'https://jsonplaceholder.typicode.com/todos'
    );
  }
}
