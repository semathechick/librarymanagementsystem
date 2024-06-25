import { Pipe, PipeTransform } from '@angular/core';
import { GetAllBook } from '../../features/models/getAllBook';

@Pipe({
  name: 'filterBookListForAuthor',
  standalone: true
})
export class FilterBookListForAuthorPipe implements PipeTransform {

  transform(value: GetAllBook[], searchKey:string): GetAllBook[] {
    if (searchKey.length < 2) return value;
    return value.filter((v) => v.authorName.includes(searchKey));

  }
}
