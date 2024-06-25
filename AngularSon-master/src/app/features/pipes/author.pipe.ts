import { Pipe, PipeTransform } from '@angular/core';

import { GetAllBook } from '../models/getAllBook';

@Pipe({
  name: 'author',
  standalone: true
})
export class AuthorPipe implements PipeTransform {

  transform(value: GetAllBook[], authorFilter:string): GetAllBook[]{
   authorFilter=authorFilter?authorFilter.toLowerCase():""
   return authorFilter?value.filter((a:GetAllBook)=>a.authorName.toLocaleLowerCase().indexOf(authorFilter)!==-1):value;

  }

}
