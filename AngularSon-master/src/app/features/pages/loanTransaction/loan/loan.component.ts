import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../services/book.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/Auth.service';
import { LoanTransaction } from '../../../models/loanTransaction';
import { LoanTransactionService } from '../../../services/loan-transaction.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-loan',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './loan.component.html',
  styleUrl: './loan.component.scss'
})
export class LoanComponent implements OnInit {
  constructor(public bookService: BookService, 
    public authService: AuthService,
    private formBuilder:FormBuilder,
    private loanService:LoanTransactionService,
    private toastr: ToastrService) { }

  loanForm !: FormGroup;
  returnDate!: Date;

  ngOnInit(): void {
    this.loanBookForm();
  }
  loanBookForm(){
    this.loanForm = this.formBuilder.group({
      bookId:[this.bookService.selectedBook.id],
      memberId:[this.authService.loggedInMember?.id],
      returnStatus:[3], 
      returnTime:['',Validators.required]
    })}

    addToDb(): void {
      if (this.loanForm.valid) {
        const formData: LoanTransaction = this.loanForm.value;
        this.loanService.borrowed(formData).subscribe({
          next: (response) => {
            console.log("Ödünç alma bilgileri", response);
            this.toastr.success(this.bookService.selectedBook.name + " isimli kitap " + this.authService.loggedInMember?.email + " kullancısına ödünç verildi");
          },
          error: (err:HttpErrorResponse) => {
            let errorMessage = 'Ödünç almada hata!';
             const match = err.error.match(/BusinessException: (.*?)\r\n/);
            if (match && match[1]) {
              errorMessage = match[1]; // Hata mesajını alıyoruz
            }
            this.toastr.error(errorMessage);
          }
        });
      }
    }


  
  }
