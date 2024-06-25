import { CommonModule } from '@angular/common';
import { Component,  OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Category } from '../../../../models/Category';
import { CategoryService } from '../../../../services/category.service';
import { ResponseModel } from '../../../../models/responseModel';
import { Router, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,RouterModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss'
})
export class CategoryListComponent implements OnInit {
  categoryEditForm !: FormGroup ;
  categoryList:Category[]=[];
  today: Date = new Date();
  searchKey : string = ' ';

  constructor(private categoryService:CategoryService, private formBuilder: FormBuilder,private router:Router){}

  ngOnInit(): void {
   
    this.getCategories();
   
    
  }

  getCategories(){
    this.categoryService.getAll().subscribe({
      next:(response:ResponseModel<Category>)=>{
        console.log('backendden cevap geldi:',response);
        this.categoryList = response.items;
        console.log("CategoryList:",this.categoryList)
        this.categoryList.forEach(category=>{
          console.log(category.categoryName);
         
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

  deleteCategory(event:any,categoryId:number){
    if(confirm('Bu kategoriyi silmek istiyor musunuz?')){
      event.target.innerText="Siliniyor...";
      this.categoryService.deleteCategory(categoryId).subscribe((res:any)=>{
        this.getCategories();
        console.log(res+" silindi.");
      });
    }
  }

  onSelectCategory(category: Category) {
    this.categoryService.selectedCategory = category; // Seçilen kategoriyi sakla
    this.router.navigate(['admin/editCategory/update/:id']); 
    console.log("OnSelectedCategory:",category);
  }
}
