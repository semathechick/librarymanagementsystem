import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BookService } from '../../../../services/book.service';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../../../services/category.service';
import { CategoryListComponent } from '../../category/category-list/category-list.component';
import { ResponseModel } from '../../../../models/responseModel';
import { Category } from '../../../../models/Category';
import { Publisher } from '../../../../models/publisher';
import { PublisherService } from '../../../../services/publisher.service';
import { Book } from '../../../../models/book';
import { AuthorService } from '../../../../services/author.service';
import { Author } from '../../../../models/Author';
import { ToastrService } from 'ngx-toastr';
import { BaseInputErrorsComponent } from '../../../../../core/components/base-input-errors/base-input-errors.component';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,CategoryListComponent, BaseInputErrorsComponent],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.scss'
})
export class AddBookComponent implements OnInit{
  
  bookId:any;
  books: Book[]=[];
  bookAddForm !: FormGroup ;
  categories:Category[]=[];
  publishers:Publisher[]=[];
  authors:Author[]=[];
  constructor(private formBuilder:FormBuilder,
    private bookService:BookService,private categoryService:CategoryService,private publisherService:PublisherService,private authorService:AuthorService, private toastr: ToastrService)
    {
     
    }
  
  
  ngOnInit(): void {
    this.createBookAddForm();
    this.getAllCategories();
    this.getAllPublishers();
    this.getAllAuthors();
   
  }
  getAllCategories() {
    this.categoryService.getAll().subscribe(
      (response: ResponseModel<Category>) => {
        this.categories=response.items;
        console.log(this.categories);
      }
    )}

    getAllPublishers(){
      this.publisherService.getAllPublisher().subscribe((response:ResponseModel<Publisher>)=>{
        this.publishers=response.items;
        console.log(this.publishers);
      })
    }
    getAllAuthors(){
      this.authorService.getAllAuthors().subscribe((response:ResponseModel<Author>)=>{
        this.authors=response.items;
        console.log(this.authors);
      })
    }
    onCategoryChange(event: any) {
      const selectedCategory = event.target.value;
      const category = this.categories.find(item=>item.id==selectedCategory);
      console.log(category);
    }
    onPublisherChange(event: any) {
      const selectedPublisher = event.target.value;
      const publisher = this.publishers.find(item=>item.id==selectedPublisher);
      console.log(publisher);
    }
    onAuthorChange(event: any) {
      const selectedAuthor = event.target.value;
      const author = this.authors.find(item=>item.id==selectedAuthor);
      console.log(author);
    }
  createBookAddForm(){
    this.bookAddForm = this.formBuilder.group({
      name:["",Validators.required],
      isbn:["",Validators.required],
      page:["",Validators.required],
      categoryId:["",Validators.required],
      publisherId:["",Validators.required],
      authorId:["",Validators.required],
      language:["",Validators.required],
      description:["",Validators.required],
      unitsInStock:["",[Validators.required,Validators.min(0)]],
    })
}


addToDb(): void {
  if (this.bookAddForm.valid) {
    const formData: Book = this.bookAddForm.value;
    console.log(formData.name);
    this.bookService.add(formData).subscribe({
      next: (response) => {
        console.log("response", response);
        this.toastr.success(formData.name.toUpperCase() + " başarıyla eklendi");
      },
      error: (err: HttpErrorResponse) => {
        let errorMessage = 'Kitap eklemede hata!';
        const match = err.error.match(/BusinessException: (.*?)\r\n/);
        if (match && match[1]) {
          errorMessage = match[1]; // Hata mesajını alıyoruz
        }
        this.toastr.error(errorMessage);
      }
    });
  } else {
    this.toastr.info("Lütfen geçerli bir kitap formu doldurun!");
  }
}}
