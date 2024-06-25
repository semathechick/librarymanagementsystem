import { Pipe, type PipeTransform } from '@angular/core';
import { Book } from '../../features/models/book';
import { GetAllBook } from '../../features/models/getAllBook';
import { Category } from '../../features/models/Category';
import { CategoryService } from '../../features/services/category.service';

@Pipe({
  name: 'FilterBookListForCategory',
  standalone: true,
})

export class FilterBookListForCategoryPipe implements PipeTransform {
  
  transform(value: GetAllBook[], searchKey:string): GetAllBook[] {
    if (searchKey.length < 2) return value;
    return value.filter((v) => v.name.includes(searchKey));

}}


