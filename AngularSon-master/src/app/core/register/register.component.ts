import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterService } from '../services/register.service';
import { Router, RouterModule } from '@angular/router';

import { CommonModule } from '@angular/common';
import { BaseInputErrorsComponent } from '../components/base-input-errors/base-input-errors.component';
import { BaseInputComponent } from '../components/base-input/base-input.component';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,RouterModule,BaseInputErrorsComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit{
  constructor(private formBuilder : FormBuilder,
    private registerService:RegisterService,private toastr:ToastrService)
    {}
    registerForm!:FormGroup;
    passwordsignUpHidden=true;

     ngOnInit(): void {
      this.createRegisterForm();
    }

    createRegisterForm(){
      this.registerForm = this.formBuilder.group({
        firstName:['',[Validators.required, Validators.minLength(2)]],
        lastName:['',[Validators.required, Validators.minLength(2),]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6),]],
       });
    }
   onRegister(){
    this.registerForm.markAsDirty();
    this.registerService.Rgstr(this.registerForm.value).subscribe(response=>{
      console.log(response);
      console.log("Başarıyla Eklendi");
     // console.log(response.firstName+""+ response.lastName+ "adlı kullanıcı eklendi.");
      this.toastr.success('Kaydınız başarılı bir şekilde gerçekleşti.');

    })
   }
  

    SignUpPasswordVisibility() {
      this.passwordsignUpHidden = !this.passwordsignUpHidden;
    }

    clear(){
     localStorage.clear() 
    }
}
