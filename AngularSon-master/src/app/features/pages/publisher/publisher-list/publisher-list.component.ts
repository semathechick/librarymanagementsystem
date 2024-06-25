import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PublisherService } from '../../../services/publisher.service';
import { Publisher } from '../../../models/publisher';
import { ResponseModel } from '../../../models/responseModel';

@Component({
  selector: 'app-publisher-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink],
  templateUrl: './publisher-list.component.html',
  styleUrl: './publisher-list.component.scss'
})
export class PublisherListComponent {

  constructor(private publisherService:PublisherService){ }
  publisherList:Publisher[]=[];
  ngOnInit(): void {
    this.getPublishers();
  }



  

  getPublishers(){
    this.publisherService.getAllPublisher().subscribe({
      next:(response:ResponseModel<Publisher>)=>{
        console.log('backendden cevap geldi:',response);
        this.publisherList = response.items;
      },
      error : (error) =>{
        console.log('backendden hatalı cevap geldi.',error);
      },
      complete: () =>{
        console.log('backend isteği sonlandı.');
      }
    });
  }

}
