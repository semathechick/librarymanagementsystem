import { Component } from '@angular/core';
import { AuthService } from '../../../../../core/services/Auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProfileService } from '../../../../services/profile.service';
import { Member } from '../../../../models/member';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterLink],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  memberUpdateForm!: FormGroup;
  constructor(public authService:AuthService,private formBuilder: FormBuilder
    ,private profileService:ProfileService){
  }
    ngOnInit(): void {
      this.editMemberUpdateForm();
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
          alert(formData.firstName.toUpperCase() + " başarıyla güncellendi");
          this.ngOnInit();
        }
        );
      }
    }


}
