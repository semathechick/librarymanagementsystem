import { Pipe, PipeTransform } from '@angular/core';
import { Book } from '../models/book';
import { GetAllBook } from '../models/getAllBook';

@Pipe({
  name: 'book',
  standalone: true
})
export class BookPipe implements PipeTransform {

  transform(value: GetAllBook[] , bookFilter:string): GetAllBook[] {
    bookFilter=bookFilter?bookFilter.toLocaleLowerCase():""
    return bookFilter?value.filter((b:GetAllBook)=>b.name.toLocaleLowerCase().indexOf(bookFilter)!==-1 
    || b.authorName.toLowerCase().indexOf(bookFilter)!==-1
    || b.isbn.toLowerCase().indexOf(bookFilter)!==-1 
    || b.categoryName.toLowerCase().indexOf(bookFilter)!==-1
    || b.publisherName.toLowerCase().indexOf(bookFilter)!==-1):value;
    
  }

}
