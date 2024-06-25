import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BookService } from '../../../../services/book.service';
import { CategoryService } from '../../../../services/category.service';
import { PublisherService } from '../../../../services/publisher.service';
import { Book } from '../../../../models/book';
import { ResponseModel } from '../../../../models/responseModel';
import { Category } from '../../../../models/Category';
import { Publisher } from '../../../../models/publisher';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from '../../category/category-list/category-list.component';
import { GetAllBook } from '../../../../models/getAllBook';
import { Response } from '../../../../models/response';
import { Author } from '../../../../models/Author';
import { AuthorService } from '../../../../services/author.service';
import { BookListComponent } from '../book-list/book-list.component';
import { SingleResponseModel } from '../../../../models/singleResponseModel';
import { ToastrService } from 'ngx-toastr';
import { BaseInputErrorsComponent } from '../../../../../core/components/base-input-errors/base-input-errors.component';


@Component({
  selector: 'app-book-update',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, CategoryListComponent, BaseInputErrorsComponent],
  templateUrl: './book-update.component.html',
  styleUrl: './book-update.component.scss'
})
export class BookUpdateComponent implements OnInit {
  
  

  bookUpdateForm!: FormGroup;
  getBook: Book[]=[];


  categories: Category[] = [];
  publishers: Publisher[] = [];
  authors: Author[]=[];
  bookId!: any;
  

  
  constructor(
    private formBuilder: FormBuilder,
    public bookService: BookService,
    private categoryService: CategoryService,
    private publisherService: PublisherService,
    private authorService:AuthorService,
    private activeToute: ActivatedRoute,
    private router:Router,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    this.getAllCategories();
    this.getAllPublishers();
    this.getAllAuthors();
    this.getBookById();
    this.editBookAddForm();
   }



  editBookAddForm(){
    this.bookUpdateForm= this.formBuilder.group({
      id:[this.bookService.selectedBook.id],
      name:["",[Validators.required, Validators.minLength(2)]],
      isbn:["",Validators.required],
      page:["",Validators.required],
      publisherId:["",Validators.required],
      categoryId:["",Validators.required],
      authorId:["",Validators.required],
      language:["",Validators.required],
      description:["",Validators.required],
      unitsInStock:["",[Validators.required,Validators.min(0)]],
    })}

   
  


  getBookById(){
    this.bookId = this.activeToute.snapshot.paramMap.get('id');
    this.bookService.getById(this.bookId).subscribe({
      next: (response:Response<Book>) => {
        this.getBook=response.items;
        console.log("Response:",response)
        
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
      }
    });
  }
  getAllCategories() {
    this.categoryService.getAll().subscribe(
      (response: ResponseModel<Category>) => {
        this.categories = response.items;
        console.log(this.categories);
      }
    );
  }

  getAllAuthors(){
    this.authorService.getAllAuthors().subscribe((response: ResponseModel<Author>)=>{
      this.authors=response.items;
      console.log(this.authors);
    })
  }

  getAllPublishers() {
    this.publisherService.getAllPublisher().subscribe((response: ResponseModel<Publisher>) => {
      this.publishers = response.items;
      console.log(this.publishers);
    });
  }
  onCategoryChange(event: any) {
    const selectedCategory = event.target.value;
    const category = this.categories.find(item => item.id == selectedCategory);
    console.log(category);
  }
  onPublisherChange(event: any) {
    const selectedPublisher = event.target.value;
    const publisher = this.publishers.find(item => item.id == selectedPublisher);
    console.log(publisher);
  }

  onAuthorChange(event: any) {
    const selectedAuthor = event.target.value;
    const author = this.authors.find(item => item.id == selectedAuthor);
    console.log(author);
  }
 
 
   onPageNumberChange(event:any){
    const selectedPageNumber = event.target.value;
     this.bookUpdateForm.patchValue({
      page: selectedPageNumber
     })
   }

   onIsbnChange(event:any){
    const selectedIsbnNumber = event.target.value;
     this.bookUpdateForm.patchValue({
      Isbn: selectedIsbnNumber
     })
   }

   onStockChange(event:any){
    const selectedStock = event.target.value;
     this.bookUpdateForm.patchValue({
      unitsInStock: selectedStock
     })
   }
   onDescriptionChange(event:any){
    const selectedDescription = event.target.value;
     this.bookUpdateForm.patchValue({
      description: selectedDescription
     })
   }
   onNameChange(event:any){
    const selectedName = event.target.value;
     this.bookUpdateForm.patchValue({
      name: selectedName
     })
   }


  updateToDb(): void {
    if (this.bookUpdateForm.valid) {
      const formData: Book = this.bookUpdateForm.value;
      console.log(formData.name);
      this.bookService.editBook(formData).subscribe((response) => {
        console.log("response", response);
        this.toastr.success(formData.name.toUpperCase() + " başarıyla güncellendi");
      }
      );
    }
  }
}
