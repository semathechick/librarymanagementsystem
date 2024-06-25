import { Component, NgModule, OnInit } from '@angular/core';
import { ReservationService } from '../../../../services/reservation.service';
import { AuthService } from '../../../../../core/services/Auth.service';
import { Reservation } from '../../../../models/Reservation';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
import { Response } from '../../../../models/response';

@Component({
  selector: 'app-reservationhistory',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './reservationhistory.component.html',
  styleUrl: './reservationhistory.component.scss'
})
export class ReservationhistoryComponent implements OnInit {
  reservations: Reservation[]=[];

  errorMessage: string = '';
  memberId?:string;
  constructor(private reservationService: ReservationService,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.memberId=this.authService.loggedInMember?.id;
    if(this.memberId)
    this.getReservations(this.memberId);
  }

  getReservations(memberId: string): void {
    this.reservationService.getReservations(memberId).subscribe({
      next: (response: Response<Reservation>) => {
        this.reservations =response.items;
        console.log('Reservations:', this.reservations); // Başarıyla alınan verileri console'a yazdırabilirsiniz
      },
      error: (error) => {
        this.errorMessage = 'Error fetching reservations';
        console.error('Error:', error); // Hata durumunda console'a hata mesajını yazdırabilirsiniz
      },
      complete: () => {
        console.log('backend isteği sonlandı.');
      }
    });
  }
  
  
}  
