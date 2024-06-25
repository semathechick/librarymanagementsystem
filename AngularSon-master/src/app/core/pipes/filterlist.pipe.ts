import { Pipe, PipeTransform } from '@angular/core';
import { GetAllBook } from '../../features/models/getAllBook';

@Pipe({
  name: 'filterlist',
  standalone: true
})
export class FilterlistPipe implements PipeTransform {

  transform(value: GetAllBook[], searchKey:string): GetAllBook[] {
    if (searchKey.length < 2) return value;
    return value.filter((v) => v.name.includes(searchKey));

}}
