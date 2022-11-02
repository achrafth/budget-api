import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Expense } from '../../Models/expense.model';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  baseApIUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

      getAllExpenses(): Observable<Expense[]> {
      return this.http.get<Expense[]>(this.baseApIUrl + '/api/expenses');
    }

      addExpense(addExpenseRequest: Expense): Observable<Expense> {
      return this.http.post<Expense>(this.baseApIUrl + '/api/expenses', addExpenseRequest);
    }

      getExpense(id: string): Observable<Expense> {
        return this.http.get<Expense>(this.baseApIUrl + '/api/expenses/' + id);
    }

      updateExpense(id: string, updateExpenseRequest: Expense): Observable<Expense> {
        return this.http.put<Expense>(this.baseApIUrl + '/api/expenses/' + id, updateExpenseRequest);
    }
      deleteExpense(id: string): Observable<Expense> {
        return this.http.delete<Expense>(this.baseApIUrl + '/api/expenses/' + id);
    }
}
