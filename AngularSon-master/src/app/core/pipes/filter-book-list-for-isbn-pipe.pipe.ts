import { Pipe, PipeTransform } from '@angular/core';
import { GetAllBook } from '../../features/models/getAllBook';

@Pipe({
  name: 'filterBookListForIsbnPipe',
  standalone: true
})
export class FilterBookListForIsbnPipePipe implements PipeTransform {

  transform(value: GetAllBook[], searchKey:string): GetAllBook[] {
    if (searchKey.length < 2) return value;
    return value.filter((v) => v.isbn.includes(searchKey));

}}
