import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Announcement } from '../../../../models/Announcement';
import { AnnouncementService } from '../../../../services/Announcement.service';
import { ToastrService } from 'ngx-toastr';
import { BaseInputComponent } from '../../../../../core/components/base-input/base-input.component';
import { BaseInputErrorsComponent } from "../../../../../core/components/base-input-errors/base-input-errors.component";
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-add-announcement',
    standalone: true,
    templateUrl: './add-announcement.component.html',
    styleUrl: './add-announcement.component.scss',
    imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule, BaseInputErrorsComponent]
})
export class AddAnnouncementComponent {
  announcementAddForm!:FormGroup;
  announcements:Announcement[]=[];

 
  constructor(private formBuilder:FormBuilder,
   private announcementService: AnnouncementService,
   private toastr: ToastrService){}
 
   ngOnInit():void{
    this.createAnnouncementAddForm();
 
   }

   createAnnouncementAddForm(){
     this.announcementAddForm=this.formBuilder.group({
       
       title:['', [Validators.required, Validators.minLength(2),]],
       description:["", Validators.required],
     })
   }
   addToDb(): void {
    if (this.announcementAddForm.valid) {
      const formData: Announcement = this.announcementAddForm.value;
      // console.log(formData.title);
      this.announcementService.add(formData).subscribe({
        next: (response) => {
          console.log("response", response);
          this.toastr.success(formData.title.toUpperCase() + " başarıyla eklendi");
        },
        error: (err: HttpErrorResponse) => {
          let errorMessage = 'Duyuru eklemede hata!';
          const match = err.error.match(/BusinessException: (.*?)\r\n/);
          if (match && match[1]) {
            errorMessage = match[1]; // Hata mesajını alıyoruz
          }
          this.toastr.error(errorMessage);
        }
      });
    } else {
      this.toastr.info("Lütfen geçerli bir duyuru formu doldurun!");
    }
  }
  

}
