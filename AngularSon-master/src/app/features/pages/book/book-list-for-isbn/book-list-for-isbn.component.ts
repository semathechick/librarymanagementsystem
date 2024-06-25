import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { GetAllBook } from '../../../models/getAllBook';
import { Category } from '../../../models/Category';
import { Publisher } from '../../../models/publisher';
import { Author } from '../../../models/Author';
import { BookService } from '../../../services/book.service';
import { CategoryService } from '../../../services/category.service';
import { PublisherService } from '../../../services/publisher.service';
import { AuthorService } from '../../../services/author.service';
import { ResponseModel } from '../../../models/responseModel';
import { NgxPaginationModule } from 'ngx-pagination';
import { AuthService } from '../../../../core/services/Auth.service';
import { FilterBookListForIsbnPipePipe } from '../../../../core/pipes/FilterBookListForIsbnPipe.pipe';

@Component({
  selector: 'app-book-list-for-isbn',
  standalone: true,
  imports: [CommonModule,FormsModule,FilterBookListForIsbnPipePipe,RouterModule,NgxPaginationModule],
  templateUrl: './book-list-for-isbn.component.html',
  styleUrl: './book-list-for-isbn.component.scss'
})
export class BookListForIsbnComponent {
  title = 'pagination';
  POSTS: any;
  page: number = 1;
  count: number = 0;
  tableSize: number = 3;
  tableSizes: any = [5, 10, 15, 20];
  
  bookList:GetAllBook[] = [];
  categoryList:Category[]=[];
  publisherList:Publisher[]=[];
  authorList : Author[]=[];
  today: Date = new Date();
  searchKey : string = '';
  constructor(private bookService : BookService,
    private categoryService:CategoryService,
    private publisherService:PublisherService,
    private authorService:AuthorService,
    private router: Router,
    private route: ActivatedRoute,
    public authService:AuthService){}
  ngOnInit(): void {

      this.getCategories();
      this.getPublishers();
      this.postList();
      this.getAuthors();
      this.getBooks();
      
  

  }

  getBooks(){
    this.bookService.getAll().subscribe({
      next:(response:ResponseModel<GetAllBook>)=>{
        console.log('backendden cevap geldi:',response);
        this.bookList = response.items;
        
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
  postList(): void {
    this.bookService.getAll().subscribe(response => {
      if (response && response.items) {
        this.POSTS = response.items;
        console.log(this.POSTS);
      }
    })
  }
  onTableDataChange(event: number): void {
    this.page = event;
    this.postList();
  }

  onTableSizeChange(event: any): void {
    this.tableSize = event.target.value;
    this.page = 1;
    this.postList();
  }

  onSelectBook(book: GetAllBook) {
    this.bookService.selectedBook = book; // Seçilen kitabı sakla
    this.router.navigate(['/loanTransaction']); // loan.html sayfasına yönlendir
  }
}
