import { Component, OnInit } from '@angular/core';
import { PublisherService } from '../../../../services/publisher.service';
import { Publisher } from '../../../../models/publisher';
import { ResponseModel } from '../../../../models/responseModel';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-publisher-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink],
  templateUrl: './publisher-list.component.html',
  styleUrl: './publisher-list.component.scss'
})
export class PublisherListComponent implements OnInit{
  constructor(private publisherService:PublisherService,
    private router:Router,
  ){ }
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
  deletePublisher(event:any,publisherId:number){
    if(confirm('Bu kitabı silmek istiyor musunuz ?')){
      event.target.innerText="Siliniyor...";
      this.publisherService.deletePublisher(publisherId).subscribe((res:any)=>{
        this.getPublishers();
        console.log(res+" silinidi");
      });
    }
  }

  onSelectPublisher(publisher: Publisher) {
    this.publisherService.selectedPublisher = publisher; // Seçilen kitabı sakla
    this.router.navigate(['admin/getPublisher/publisher/:id/publisherupdate']); 
    console.log("OnSelectedPublisher:",publisher);
  }
}
