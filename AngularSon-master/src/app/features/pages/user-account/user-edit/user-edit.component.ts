import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../../../core/services/Auth.service';
import { Member } from '../../../models/member';
import { ProfileService } from '../../../services/profile.service';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-edit',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterLink],
  templateUrl: './user-edit.component.html',
  styleUrl: './user-edit.component.scss'
})
export class UserEditComponent {
  ngOnInit(): void {
    this.editMemberUpdateForm();
  }

  memberUpdateForm!: FormGroup;
  constructor(public authService:AuthService,private formBuilder: FormBuilder
    ,private profileService:ProfileService, private toastr:ToastrService){
  }
  editMemberUpdateForm(){
    this.memberUpdateForm= this.formBuilder.group({
      id:[this.authService.loggedInMember?.id],
      firstName:[this.authService.loggedInMember?.firstName,[Validators.required, Validators.minLength(2)]],
      lastName:[this.authService.loggedInMember?.lastName,[Validators.required, Validators.minLength(2)]],
      email:[this.authService.loggedInMember?.email],
    })}
    
    updateToDb(): void {
      if (this.memberUpdateForm.valid) {
        const formData: Member = this.memberUpdateForm.value;
        console.log(formData.firstName);
        this.profileService.editMemberProfile(formData).subscribe((response) => {
          console.log("response", response);
          this.toastr.success(formData.firstName.toUpperCase() + " başarıyla güncellendi");
          this.ngOnInit();
        }
        );
      }
    }
}