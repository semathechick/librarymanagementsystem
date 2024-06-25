import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CategoryService } from '../../../features/services/category.service';
import { AuthorService } from '../../../features/services/author.service';
import { Category } from '../../../features/models/Category';
import { Author } from '../../../features/models/Author';
import { ResponseModel } from '../../../features/models/responseModel';
import { BookListForMembersComponent } from '../../../features/pages/book/book-list-for-members/book-list-for-members.component';
import { BookService } from '../../../features/services/book.service';
import { GetAllBook } from '../../../features/models/getAllBook';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bottombar',
  standalone: true,
  imports: [CommonModule,RouterModule,BookListForMembersComponent],
  templateUrl: './bottombar.component.html',
  styleUrl: './bottombar.component.scss'
})
export class BottombarComponent {
  constructor(private catService:CategoryService,private activatedRoute: ActivatedRoute,private authorService:AuthorService,private bookService : BookService,  private router:Router,){}

  bookList:GetAllBook[] = [];

  ngOnInit(): void {

    this.activatedRoute.params.subscribe(params => {
      if (params["categoryId"]) {
        this.getBooksByCategoryId(params['categoryId']);
      }
      else if (params["authorId"]) {
        this.getBooksByAuthorId(params['authorId']);
      }
      else {
        this.getAllCategories();
       
        this.getAllAuthors();
        
      }
    })
    
}

  categories:Category[]=[];
  
  currentCategory!:Category;
  authors:Author[]=[];
  currentAuthor!:Author;

  getAllCategories() {
    this.catService.getAll().subscribe(
      (response: ResponseModel<Category>) => {
        this.categories = response.items;
        this.categories
      }
    )}


    setCurrentCategory(category:Category){
      this.currentCategory=category;
      console.log(this.currentCategory);
    }
    getCurrentCategory(category:Category){
      if(category==this.currentCategory){
        return "list-group-item active"
      }
      else{
        return "list-group-item"
      }
    }

    getAllAuthors() {
      this.authorService.getAllAuthors().subscribe(
        (response: ResponseModel<Author>) => {
          this.authors = response.items;
          this.authors
        }
      )}

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
  
  
      setCurrentAuthor(author:Author){
        this.currentAuthor=author;
        console.log(this.currentAuthor);
      }
      getCurrentAuthor(author:Author){
        if(author==this.currentAuthor){
          return "list-group-item active"
        }
        else{
          return "list-group-item"
        }
      }

}
