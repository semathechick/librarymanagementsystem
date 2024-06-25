import { Pipe, type PipeTransform } from '@angular/core';

import { GetAllBook } from '../../features/models/getAllBook';

@Pipe({
  name: 'FilterBookListForIsbnPipe',
  standalone: true,
})
export class FilterBookListForIsbnPipePipe implements PipeTransform {

  transform(value: GetAllBook[], searchKey:string): GetAllBook[] {
    if (searchKey.length < 2) return value;
    return value.filter((v) => v.isbn.includes(searchKey));

}}
