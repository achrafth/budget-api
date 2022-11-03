import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Expense } from 'src/app/Models/expense.model';
import { ExpensesService } from 'src/app/Services/expenses/expenses.service';

@Component({
  selector: 'app-expenses-list',
  templateUrl: './expenses-list.component.html',
  styleUrls: ['./expenses-list.component.scss']
})
export class ExpensesListComponent implements OnInit {

  expenses: Expense[] = [];
  constructor(private expensesService: ExpensesService) { }

  ngOnInit(): void {
 this.expensesService.getAllExpenses().subscribe({
      next: (expenses) => {
          this.expenses = expenses;
          console.log(expenses)
      },
      error: (response) => {
    console.log(response);
      }
    });
  }
}
