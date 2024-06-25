import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CategoryService } from '../../../services/category.service';
import { Category } from '../../../models/Category';
import { ResponseModel } from '../../../models/responseModel';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [RouterModule,HttpClientModule,CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent {
  constructor(private catService:CategoryService){}
  ngOnInit(): void {
    this.getAllCategories();
  }

  categories:Category[]=[];
  currentCategory!:Category;

  getAllCategories() {
    this.catService.getAll().subscribe(
      (response: ResponseModel<Category>) => {
        this.categories = response.items;
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

}
