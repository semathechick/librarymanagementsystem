import { Component } from '@angular/core';
import { Author } from '../../../../models/Author';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthorService } from '../../../../services/author.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Response } from '../../../../models/response';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { BaseInputErrorsComponent } from '../../../../../core/components/base-input-errors/base-input-errors.component';

@Component({
  selector: 'app-update-author',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,RouterModule, BaseInputErrorsComponent],
  templateUrl: './update-author.component.html',
  styleUrl: './update-author.component.scss'
})
export class UpdateAuthorComponent {

  authorUpdateForm!: FormGroup;
  getAuthors: Author[] = [];
  authorId:any;

  
  
  
  constructor(
    private formBuilder: FormBuilder,
    public authorService: AuthorService,
   private activeRoute: ActivatedRoute,
    private route: Router, 
    private toastr:ToastrService) { }


  ngOnInit(): void {
    
    this.getAuthorById();
    this.updateAuthorAddForm();
   
    
  }


  getAuthorById(){
    this.authorId=this.activeRoute.snapshot.paramMap.get('id');
    this.authorService.getById(this.authorId).subscribe({
      next:(response:Response<Author>)=>{
        this.getAuthors=response.items;
        console.log("Response:",response);
      },
      error:(error)=>{
        console.log(error);
      },
      complete:()=>{}
   });
  }

  updateAuthorAddForm(){
    this.authorUpdateForm= this.formBuilder.group({
      id:[this.authorService.selectedAuthor.id],
      name:["",[Validators.required, Validators.minLength(2)]],
      identityNumber:["", (Validators.required)],
       biography:["", (Validators.required)],
      
    })}

   
  onNameChange(event:any){
    const selectedName = event.target.value;
     this.authorUpdateForm.patchValue({
      name: selectedName
     })
   }



  updateToDb(): void {
    if (this.authorUpdateForm.valid) {
      const formData: Author = this.authorUpdateForm.value;
      console.log(formData.name);
      this.authorService.editAuthor(formData).subscribe((response) => {
        console.log("response", response);
        this.toastr.success(formData.name.toUpperCase() + " başarıyla güncellendi");
      }
      );
    }
  }

  onIdentityChange(event:any){
    const selectedIdentity = event.target.value;
     this.authorUpdateForm.patchValue({
      identity: selectedIdentity
     })
   }
   onBiographyChange(event:any){
    const selectedbiography = event.target.value;
     this.authorUpdateForm.patchValue({
      biography: selectedbiography
     })
   }
   

}
