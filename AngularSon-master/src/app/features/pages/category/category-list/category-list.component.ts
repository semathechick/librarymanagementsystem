import { CommonModule } from '@angular/common';
import { Component,  OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../models/Category';
import { CategoryService } from '../../../services/category.service';
import { ResponseModel } from '../../../models/responseModel';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss'
})
export class CategoryListComponent implements OnInit {
  categoryList: Category[] = [];
  constructor(private categoryService : CategoryService){}
  
  ngOnInit(): void {
    this.getCategories();
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
}
