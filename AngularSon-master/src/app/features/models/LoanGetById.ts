export interface LoanGetById {
    id: string;
    memberId: string;
    bookId: string;
    returnStatus: ReturnStatus;
    returnTime: Date;
}
export enum ReturnStatus {
    NotReturned = 1,
    Returned = 2,
    Borrowed = 3
}