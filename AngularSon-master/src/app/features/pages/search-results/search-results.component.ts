import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../services/book.service';
import { GetAllBook } from '../../models/getAllBook';
import { Category } from '../../models/Category';
import { Publisher } from '../../models/publisher';
import { Author } from '../../models/Author';
import { PublisherService } from '../../services/publisher.service';
import { CategoryService } from '../../services/category.service';
import { AuthorService } from '../../services/author.service';
import { AuthService } from '../../../core/services/Auth.service';
import { forkJoin } from 'rxjs';
import { ResponseModel } from '../../models/responseModel';
import { NgxPaginationModule } from 'ngx-pagination';
import { FilterBookListForCategoryPipe } from '../../../core/pipes/FilterBookListForCategory.pipe';
import { BookPipe } from '../../pipes/book.pipe';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-results',
  standalone: true,
  imports: [FormsModule, CommonModule, NgxPaginationModule, FilterBookListForCategoryPipe, BookPipe],
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss']
})
export class SearchResultsComponent implements OnInit {

  title = 'pagination';
  POSTS: any;
  page: number = 1;
  count: number = 0;
  tableSize: number = 3;
  tableSizes: any = [5, 10, 15, 20];

  bookList: GetAllBook[] = [];
  categoryList: Category[] = [];
  publisherList: Publisher[] = [];
  authorList: Author[] = [];
  bookFilter: string = '';

  constructor(
    private bookService: BookService, private router: Router, private categoryService: CategoryService,
    private publisherService: PublisherService, private authorService: AuthorService,
    private activatedRoute: ActivatedRoute, public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.bookFilter = params['filter'] || '';
      this.getBooks();
    });
  }

  getBooks(): void {
    forkJoin({
      categories: this.categoryService.getAll(),
      publishers: this.publisherService.getAllPublisher(),
      authors: this.authorService.getAllAuthors(),
      books: this.bookService.getAll()
    }).subscribe(({ categories, publishers, authors, books }) => {
      this.categoryList = categories.items;
      this.publisherList = publishers.items;
      this.authorList = authors.items;
      this.bookList = books.items;

      this.bookList.forEach(book => {
        const category = this.categoryList.find(category => category.id === book.categoryId);
        const publisher = this.publisherList.find(publisher => publisher.id === book.publisherId);
        const author = this.authorList.find(author => author.id === book.authorId);

        if (category) book.categoryName = category.categoryName;
        if (publisher) book.publisherName = publisher.name;
        if (author) book.authorName = author.name;
      });

      if (this.bookFilter) {
        this.bookList = this.bookList.filter(book =>
          book.name.toLocaleLowerCase().includes(this.bookFilter.toLocaleLowerCase()) ||
          book.authorName.toLocaleLowerCase().includes(this.bookFilter.toLocaleLowerCase()) ||
          book.isbn.toLocaleLowerCase().includes(this.bookFilter.toLocaleLowerCase()) ||
          book.categoryName.toLocaleLowerCase().includes(this.bookFilter.toLocaleLowerCase()) ||
          book.publisherName.toLocaleLowerCase().includes(this.bookFilter.toLocaleLowerCase())
        );
      }
    }, error => {
      console.log(error);
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
