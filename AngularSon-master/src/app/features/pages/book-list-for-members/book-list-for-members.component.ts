import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";

import { BookService } from "../../services/book.service";
import { ResponseModel } from "../../models/responseModel";
import { GetAllBook } from "../../models/getAllBook";
import { CategoryService } from "../../services/category.service";
import { Category } from "../../models/Category";
import { PublisherService } from "../../services/publisher.service";
import { Publisher } from "../../models/publisher";
import { ActivatedRoute, RouterLink, RouterModule } from "@angular/router";
import { FilterlistPipe } from "../../../core/pipes/filterlist.pipe";
import { FilterBookListForCategoryPipe } from "../../../core/pipes/FilterBookListForCategory.pipe";
import { Book } from "../../models/book";
import { AuthorService } from "../../services/author.service";
import { Author } from "../../models/Author";
import { NgxPaginationModule } from "ngx-pagination";




@Component({
    selector: 'app-book-list-for-members',
    standalone: true,
    templateUrl: './book-list-for-members.component.html',
    styleUrl: './book-list-for-members.component.scss',
    imports: [CommonModule, FormsModule, RouterLink, RouterModule,NgxPaginationModule,FilterlistPipe,FilterBookListForCategoryPipe]
})
export class BookListComponentForMembers implements OnInit{
  title='pagination';
  POSTS:any;
  page:number=1;
  count:number=0;
  tableSize:number=3;
  tableSizes:any=[5,10,15,20];


  bookList:GetAllBook[] = [];
  categoryList:Category[]=[];
  publisherList:Publisher[]=[];
  authorList:Author[]=[];
  today: Date = new Date();
  searchKey : string = '';
  constructor(private bookService : BookService,private categoryService:CategoryService,private publisherService:PublisherService,private authorService:AuthorService,private activatedRoute:ActivatedRoute){}
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params=>{
      if(params["categoryId"]){
        this.getBooksByCategoryId(params['categoryId']);
        }
        else if(params["authorId"]){
          this.getBooksByAuthorId(params['authorId']);
        }
        else{
          this.getCategories();
          this.getPublishers();
          this.getBooks();
          this.getAuthors();
          this.postList();
        }
    })
    
  
  }

  getBooks(){
    this.bookService.getAll().subscribe({
      next:(response:ResponseModel<GetAllBook>)=>{
        console.log('backendden cevap geldi:',response);
        this.bookList = response.items;
        console.log("BookList:",this.bookList)
        this.bookList.forEach(book=>{
          console.log(book.name);
          let categoryId=book.categoryId
          let publisherId=book.publisherId;
          let authorId=book.authorId;
          const category=this.categoryList.find(category=>category.id===categoryId);
          const publisher=this.publisherList.find(publisher=>publisher.id===publisherId);
          const author=this.authorList.find(author=>author.id===authorId);
          if(category && publisher && author){
            
            book.categoryName=category.categoryName;
            book.publisherName=publisher.name;
            book.authorName= author.name;
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
  getAuthors(){
    this.authorService.getAllAuthors().subscribe({
      next:(response:ResponseModel<Author>)=>{
        console.log('backendden cevap geldi:',response);
        this.authorList = response.items;
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
        console.log(res+" silinidi");
      });
    }
  }

  getBooksByCategoryId(categoryId:number){
    this.bookService.getBooksByCategoryId(categoryId).subscribe((response)=>
    {
      this.bookList = response.items;
    },
    error=>{
      console.log(error)
    }
  )
}
getBooksByAuthorId(authorId:number){
  this.bookService.getBooksByAuthorId(authorId).subscribe((response)=>
  {
    this.bookList = response.items;
  },
  error=>{
    console.log(error)
  }
)
 }

 postList():void{
  this.bookService.getAll().subscribe(response=>{
    if(response && response.items){
      this.POSTS=response.items;
    console.log(this.POSTS);
    }
  })
}
onTableDataChange(event:number):void {
  this.page=event;
  this.postList();
}

onTableSizeChange(event:any):void{
  this.tableSize=event.target.value;
  this.page=1;
  this.postList();
}
}
