import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Publisher } from '../../../../models/publisher'; 
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PublisherService } from '../../../../services/publisher.service'; 
import { ResponseModel } from '../../../../models/responseModel';
import { ToastrService } from 'ngx-toastr';
import { BaseInputErrorsComponent } from '../../../../../core/components/base-input-errors/base-input-errors.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-publisher',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,RouterModule,BaseInputErrorsComponent],
  templateUrl: './publisherupdate.component.html',
  styleUrl: './publisherupdate.component.scss'
})
export class publisherUpdateComponent {
  publisherUpdateForm!: FormGroup;
  getPublisher: Publisher[]=[];
  publisherId:any;

  
  constructor(
    private formBuilder: FormBuilder,
    public publisherService: PublisherService,
    private activeRoute: ActivatedRoute,
    private route: Router,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    
   this.getById();
    this.updatePublisherAddForm();
    
  }
  updatePublisherAddForm(){
    this.publisherUpdateForm= this.formBuilder.group({
      id:[this.publisherService.selectedPublisher.id],
      name:["",[Validators.required, Validators.minLength(2)]],
      
    })}

 
    onNameChange(event:any){
      const selectedName = event.target.value;
       this.publisherUpdateForm.patchValue({
        name: selectedName
       })
     }

  getById(){
    this.publisherId = this.activeRoute.snapshot.paramMap.get('id');
    this.publisherService.getById(this.publisherId).subscribe({
      next: (response:ResponseModel<Publisher>) => {
        this.getPublisher=response.items;
        console.log("Response:",response)
        
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
      }
    });
  }
  updateToDb(): void {
    if (this.publisherUpdateForm.valid) {
      const formData: Publisher = this.publisherUpdateForm.value;
      console.log(formData.name);
      this.publisherService.editPublisher(formData).subscribe((response) => {
        console.log("response", response);
        this.toastr.success(formData.name.toUpperCase() + " başarıyla güncellendi");
      }
      );
    }
  }

  

}