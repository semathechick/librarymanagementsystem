import { Component } from '@angular/core';
import { Author } from '../../../../models/Author';
import { AuthorService } from '../../../../services/author.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ResponseModel } from '../../../../models/responseModel';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-author',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule,ReactiveFormsModule],
  templateUrl: './edit-author.component.html',
  styleUrl: './edit-author.component.scss'
})
export class EditAuthorComponent {

  authorEditForm !: FormGroup ;
  authorList:Author[]=[];
  today: Date = new Date();
  searchKey : string = ' ';

  constructor(private authorService:AuthorService, private formBuilder: FormBuilder,private router:Router){}

  ngOnInit(): void {
   
    this.getAuthors();
   
    
  }

  getAuthors(){
    this.authorService.getAllAuthors().subscribe({
      next:(response:ResponseModel<Author>)=>{
        console.log('backendden cevap geldi:',response);
        this.authorList = response.items;
        console.log("AuthorList:",this.authorList)
        this.authorList.forEach(author=>{
          console.log(author.name);
         
        })
      },
      error : (error) =>{
        console.log('backendden hatalı cevap geldi.',error);
      },
      complete: () =>{
        console.log('backend isteği sonlandı.');
      }
    });
  }
/* 
  createCategoryEditForm(){
    this.categoryEditForm=this.formBuilder.group({
      categoryName:["",Validators.required, Validators.minLength(2)]
    })
  } */

  deleteAuthor(event:any,authorId:number){
    if(confirm('Bu yazarı silmek istiyor musunuz ?')){
      event.target.innerText="Siliniyor...";
      this.authorService.deleteAuthor(authorId).subscribe((res:any)=>{
        this.getAuthors();
        console.log(res+" silindi.");
      });
    }
  }

  onSelectAuthor(author: Author) {
    this.authorService.selectedAuthor = author; // Seçilen kitabı sakla
    this.router.navigate(['admin/editAuthor/update/:id']); 
    console.log("OnSelectedAuthor:",author);
  }
}
