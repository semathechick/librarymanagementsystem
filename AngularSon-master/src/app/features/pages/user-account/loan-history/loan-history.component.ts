import { Component, OnInit } from '@angular/core';
import { LoanTransactionService } from '../../../services/loan-transaction.service';
import { AuthService } from '../../../../core/services/Auth.service';
import { LoanTransaction } from '../../../models/loanTransaction';
import { ResponseModel } from '../../../models/responseModel';
import { ReturnLoanBook } from '../../../models/returnLoanBook';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-loan-history',
  standalone: true,
  imports: [CommonModule,RouterModule,ReactiveFormsModule,FormsModule],
  templateUrl: './loan-history.component.html',
  styleUrls: ['./loan-history.component.scss']
})
export class LoanHistoryComponent implements OnInit {
  memberId = this.authService.loggedInMember?.id;
  loanList: LoanTransaction[] = [];
  myResponse: LoanTransaction[] = [];
  myResponseBorrowed: LoanTransaction[] = [];
  penalty: number = 0;

  constructor(
    private loanService: LoanTransactionService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getLoans();
  }

  getLoans() {
    this.loanService.getAll().subscribe({
      next: (response: ResponseModel<LoanTransaction>) => {
        console.log('Backend response:', response);
        this.loanList = response.items;
        this.filterLoansForMember();
      },
      error: (error) => {
        console.log('Backend error:', error);
      },
      complete: () => {
        console.log('Backend request completed.');
      }
    });
  }

  filterLoansForMember() {
    this.myResponse = this.loanList.filter(loan => loan.memberId === this.memberId);
    this.myResponseBorrowed = this.myResponse.filter(loan => loan.returnStatus === 3);
  }

  bookReturn(item: LoanTransaction): void {
    const formData: ReturnLoanBook = {
      id: item.id, 
      bookId: item.bookId, 
      memberId: this.authService.loggedInMember ? this.authService.loggedInMember.id : '', 
      returnStatus: 2
    };
    this.loanService.bookReturn(formData).subscribe(() => {
      alert("Kitap iade edildi");
      this.myResponseBorrowed = this.myResponseBorrowed.filter(loan => loan.id !== item.id);
    });
  }

  getReturnTimeMessage(returnTime: Date): string {
    const currentTime = new Date();
    const myReturnTime = new Date(returnTime);
  
    console.log("Current time:", currentTime.toLocaleString(), "Return time:", myReturnTime.toLocaleString());
  
    const timeDifference = myReturnTime.getTime() - currentTime.getTime();
    const daysDifference = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
    const daysDifferenceAbs = Math.abs(daysDifference);

    if (timeDifference < 0) {
      this.penalty = daysDifferenceAbs * 3;
      return `İade süresi ${daysDifferenceAbs} gün geçti. Ceza tutarı: ${this.penalty} TL`;
    } else if (timeDifference > 0) {
      return `İade süresinin dolmasına ${daysDifferenceAbs} gün kaldı.`;
    } else {
      return "İade süresi bugün doluyor.";
    }
  }

  calculateTotalPenalty(): number {
    return this.myResponseBorrowed.reduce((total, loan) => total + (this.getPenaltyForLoan(loan)), 0);
  }

  getPenaltyForLoan(loan: LoanTransaction): number {
    const returnTime = new Date(loan.returnTime);
    const currentTime = new Date();
    const timeDifference = returnTime.getTime() - currentTime.getTime();
    const daysDifference = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
    const daysDifferenceAbs = Math.abs(daysDifference);

    if (timeDifference < 0) {
      return daysDifferenceAbs * 3;
    } else {
      return 0;
    }
  }
}
