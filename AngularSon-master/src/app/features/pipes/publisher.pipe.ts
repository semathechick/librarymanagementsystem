import { Pipe, PipeTransform } from '@angular/core';
import { Publisher } from '../models/publisher';
import { GetAllBook } from '../models/getAllBook';

@Pipe({
  name: 'publisher',
  standalone: true
})
export class PublisherPipe implements PipeTransform {

  transform(value: GetAllBook[], publisherFilter:string): GetAllBook[]{
    publisherFilter=publisherFilter?publisherFilter.toLocaleLowerCase():""
    return publisherFilter?value.filter((p:Publisher)=>p.name.toLocaleLowerCase().indexOf(publisherFilter)!==-1):value;
  }

}
