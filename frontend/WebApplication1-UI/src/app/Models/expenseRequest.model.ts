export interface ExpenseRequest {
    typeExpense: string;
    dateTime: Date;
    total: number;
    paidBy: string;
}