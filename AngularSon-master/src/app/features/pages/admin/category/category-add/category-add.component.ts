import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Category } from '../../../../models/Category';
import { CategoryService } from '../../../../services/category.service';
import { ToastrService } from 'ngx-toastr';
import { BaseInputErrorsComponent } from '../../../../../core/components/base-input-errors/base-input-errors.component';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-category-add',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule,ReactiveFormsModule,BaseInputErrorsComponent],
  templateUrl: './category-add.component.html',
  styleUrl: './category-add.component.scss'
})
export class CategoryAddComponent {
  categoryAddForm!:FormGroup;
  categories:Category[]=[];

 
  constructor(private formBuilder:FormBuilder,
   private categoryService: CategoryService,
   private toastr: ToastrService){}
 
   ngOnInit():void{
    this.createCategoryAddForm();
 
   }

   createCategoryAddForm(){
     this.categoryAddForm=this.formBuilder.group({
       categoryName:["",[Validators.required, Validators.minLength(2)]],
     })
   }
   addToDb(): void {
    if (this.categoryAddForm.valid) {
      const formData: Category = this.categoryAddForm.value;
      console.log(formData.categoryName);
      this.categoryService.add(formData).subscribe({
        next: (response) => {
          console.log("response", response);
          this.toastr.success(formData.categoryName.toUpperCase() + " başarıyla eklendi");
        },
        error: (err: HttpErrorResponse) => {
          let errorMessage = 'Kategori eklemede hata!';
          const match = err.error.match(/BusinessException: (.*?)\r\n/);
          if (match && match[1]) {
            errorMessage = match[1]; // Hata mesajını alıyoruz
          }
          this.toastr.error(errorMessage);
        }
      });
    } else {
      this.toastr.info("Lütfen geçerli bir kategori formu doldurun!");
    }
  }
}
