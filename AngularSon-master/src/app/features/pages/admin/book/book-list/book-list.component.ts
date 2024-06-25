import { CommonModule } from "@angular/common";
import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";

import { BookService } from "../../../../services/book.service";
import { ResponseModel } from "../../../../models/responseModel";
import { GetAllBook } from "../../../../models/getAllBook";
import { CategoryService } from "../../../../services/category.service";
import { Category } from "../../../../models/Category";
import { PublisherService } from "../../../../services/publisher.service";
import { Publisher } from "../../../../models/publisher";
import { Router, RouterLink, RouterModule } from "@angular/router";
import { FilterlistPipe } from "../../../../../core/pipes/filterlist.pipe";
import { FilterBookListForCategoryPipe } from "../../../../../core/pipes/FilterBookListForCategory.pipe";
import { AuthorService } from "../../../../services/author.service";
import { Author } from "../../../../models/Author";
import { BookUpdateComponent } from "../book-update/book-update.component";
import { ToastrService } from "ngx-toastr";





@Component({
    selector: 'app-book-list',
    standalone: true,
    templateUrl: './book-list.component.html',
    styleUrl: './book-list.component.scss',
    imports: [CommonModule, FormsModule, RouterLink, RouterModule,FilterlistPipe,FilterBookListForCategoryPipe]
})
export class BookListComponent implements OnInit{
  
  

  bookList:GetAllBook[] = [];
  categoryList:Category[]=[];
  publisherList:Publisher[]=[];
  authorList: Author[]=[];
  today: Date = new Date();
  searchKey : string = ' ';

  constructor(private bookService : BookService,
    private categoryService:CategoryService,
    private publisherService:PublisherService,
    private authorService: AuthorService,
    private router:Router,
    private toastr: ToastrService
    )
  {
    
  }


  ngOnInit(): void {
    this.getAuthors();
    this.getCategories();
    this.getPublishers();
    this.getBooks();
    
  }
  onSelectBook(book: GetAllBook) {
    this.bookService.selectedBook = book; // Seçilen kitabı sakla
    this.router.navigate(['admin/getBooks/book-update']); 
    console.log("OnSelectedBook:",book);
  }
  getBooks(){
    this.bookService.getAll().subscribe({
      next:(response:ResponseModel<GetAllBook>)=>{
        console.log('backendden cevap geldi:',response);
        this.bookList = response.items;
        console.log("BookList:",this.bookList)
        this.bookList.forEach(book=>{
          console.log(book.name);
          let categoryId=book.categoryId;
          let publisherId=book.publisherId;
          let authorId=book.authorId;
          const category=this.categoryList.find(category=>category.id===categoryId);
          const publisher=this.publisherList.find(publisher=>publisher.id===publisherId);
          const author=this.authorList.find(author=>author.id===authorId);
           
          if(category && publisher && author){
            
            book.categoryName=category.categoryName;
            book.publisherName=publisher.name;
            book.authorName=author.name;
          }
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

  getAuthors(){
    this.authorService.getAllAuthors().subscribe({
      next:(response:ResponseModel<Author>)=>{
        console.log("Backendten cevap geldi:", response);
        this.authorList=response.items;
      },
      error: (error)=>{
        console.log('backendten hatalı cevap geldi.');
      },
      complete:()=>{
        console.log('backend isteği sonlandı.');
      }
    });
  }

  getCategories(){
    this.categoryService.getAll().subscribe({
      next:(response:ResponseModel<Category>)=>{
        console.log('backendden cevap geldi:',response);
        this.categoryList = response.items;
      },
      error : (error) =>{
        console.log('backendden hatalı cevap geldi.',error);
      },
      complete: () =>{
        console.log('backend isteği sonlandı.');
      }
    });
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
  deleteBook(event:any,bookId:number){
    if(confirm('Bu kitabı silmek istiyor musunuz ?')){
      event.target.innerText="Siliniyor...";
      this.bookService.deleteBook(bookId).subscribe((res:any)=>{
        this.getBooks();
        console.log(res+" silindi");
      });
    }
  }

}
