export interface Expense {
    expenseId: string;
    type: string;
    dateTime: Date;
    total: number;
    paidBy: string;
}