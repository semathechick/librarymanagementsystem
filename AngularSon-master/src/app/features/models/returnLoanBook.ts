export interface ReturnLoanBook {
    id: string;
    memberId: string;
    bookId: string;
    returnStatus: ReturnStatus;    
}
export enum ReturnStatus {
    NotReturned = 1,
    Returned = 2,
    Borrowed = 3
}