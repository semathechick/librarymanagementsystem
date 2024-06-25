import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../../services/reservation.service';
import { Reservation } from '../../../models/Reservation';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../../core/services/Auth.service';
import { LoanTransactionService } from '../../../services/loan-transaction.service';
import { BookService } from '../../../services/book.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent  implements OnInit{
  constructor(private reservationService:ReservationService,
    public authService: AuthService,
    private formBuilder:FormBuilder,
    private toastr: ToastrService,
    public bookService: BookService
   ){}
  ngOnInit(): void {
   this.createReservationForm();
  }
   reservationForm!: FormGroup;
   reservations: Reservation[] = [];

   createReservationForm(): void {
    this.reservationForm = this.formBuilder.group({
      bookId: [this.bookService.selectedBook.id, Validators.required],
      memberId: [this.authService.loggedInMember?.id, Validators.required],
      rezervationDate: [Validators.required],
      expirationDate: [Validators.required] 
    });
  }
  onSubmit(): void {
    if (this.reservationForm.valid) {
      this.reservationService.createReservation(this.reservationForm.value)
        .subscribe({
          next: (response) => {
            this.toastr.success(`${this.bookService.selectedBook.name} isimli kitap ${this.authService.loggedInMember?.email} maili kullanıcıya rezerve edildi`);
            console.log("Rezerve bilgileri:",response);
          },
          error: (error) => {
            console.error('Rezervasyon oluşturulurken hata:', error);
            // Hata durumunu kullanıcıya bildirin
            this.toastr.error('Rezervasyon oluşturulurken bir hata oluştu.');
          }
        });
    }
  }
  getReservations(): void {
    const bookId = this.reservationForm.get('bookId')!.value; // Non-null assertion operator (!) kullanımı
    if (bookId) {
      this.reservationService.getReservations(bookId).subscribe(
        data => {
          this.reservations = data.items;
        },
        error => {
          // Hata durumunu yönetin.
          console.error('Rezervasyonlar yüklenirken hata:', error);
        }
      );
    } else {
      console.error('Book ID is null or undefined');
    }
  }
  
}

