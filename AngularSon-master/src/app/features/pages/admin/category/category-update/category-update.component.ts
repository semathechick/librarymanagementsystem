import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Category } from '../../../../models/Category';
import { CategoryService } from '../../../../services/category.service';
import { CommonModule } from '@angular/common';
import { Response } from '../../../../models/response';
import { ToastrService } from 'ngx-toastr';
import { BaseInputErrorsComponent } from '../../../../../core/components/base-input-errors/base-input-errors.component';

@Component({
  selector: 'app-category-update',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,RouterModule, BaseInputErrorsComponent],
  templateUrl: './category-update.component.html',
  styleUrl: './category-update.component.scss'
})
export class CategoryUpdateComponent {

  categoryUpdateForm!: FormGroup;
  getCategories: Category[] = [];
  categoryId:any;

  constructor(
    private formBuilder: FormBuilder,
    public categoryService: CategoryService,
   private activeRoute: ActivatedRoute,
    private route: Router,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    
    this.getCategoryById();
    this.updateCategoryAddForm();
   
    
  }


  getCategoryById(){
    this.categoryId=this.activeRoute.snapshot.paramMap.get('id');
    this.categoryService.getById(this.categoryId).subscribe({
      next:(response:Response<Category>)=>{
        this.getCategories=response.items;
        console.log("Response:",response);
      },
      error:(error)=>{
        console.log(error);
      },
      complete:()=>{}
   });
  }

  updateCategoryAddForm(){
    this.categoryUpdateForm= this.formBuilder.group({
      id:[this.categoryService.selectedCategory.id],
      categoryName:["",[Validators.required, Validators.minLength(2)]],
      
    })}

  
  onNameChange(event:any){
    const selectedName = event.target.value;
     this.categoryUpdateForm.patchValue({
      categoryName: selectedName
     })
   }



  updateToDb(): void {
    if (this.categoryUpdateForm.valid) {
      const formData: Category = this.categoryUpdateForm.value;
      console.log(formData.categoryName);
      this.categoryService.editCategory(formData).subscribe((response) => {
        console.log("response", response);
        this.toastr.success(formData.categoryName.toUpperCase() + " başarıyla güncellendi");
      }
      );
    }
  }

}
