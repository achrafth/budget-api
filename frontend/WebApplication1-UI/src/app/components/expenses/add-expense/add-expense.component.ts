import { Component, OnInit } from '@angular/core';
import { Route, Router, RouterEvent } from '@angular/router';
import { Expense } from 'src/app/Models/expense.model';
import { ExpensesService } from 'src/app/Services/expenses/expenses.service';
import { UsersService } from 'src/app/Services/users/users.service';

@Component({
  selector: 'app-add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {

  expenseTypes = ['FOOD', 'CLOTHES', 'HOTEL', 'TRANSPORT', 'OTHERS'];
  usersNames: string[] = [];
  addExpenseRequest: Expense = {
    expenseId: '',
    type: '',
    dateTime: new Date(),
    total: 0,
    paidBy: ''
  }

  constructor(private expensesService: ExpensesService, private usersService: UsersService, private router: Router) {}

  ngOnInit(): void {
    this.usersService.getUsersNames().subscribe({
      next: (userNames) => {
          this.usersNames = userNames;
          console.log(userNames)
      },
      error: (response) => {
    console.log(response);
      }
    });
  }

  addExpense(){
    this.expensesService.addExpense(this.addExpenseRequest) 
    .subscribe({
      next: (response) => {
        console.log(response);
        
        this.router.navigate(['expenses']); 
      }
    })
  }
}
