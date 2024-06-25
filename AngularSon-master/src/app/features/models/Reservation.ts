export interface Reservation {
    id: string;
    bookId: string;
    memberId: string;
    bookName:string;
    rezervationDate: Date;
    expirationDate: Date;
  }