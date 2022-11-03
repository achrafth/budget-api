import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Expense } from 'src/app/Models/expense.model';
import { ExpensesService } from 'src/app/Services/expenses/expenses.service';
import { UsersService } from 'src/app/Services/users/users.service';

@Component({
  selector: 'app-update-expense',
  templateUrl: './update-expense.component.html',
  styleUrls: ['./update-expense.component.css']
})
export class UpdateExpenseComponent implements OnInit {

  expenseTypes = ['Food', 'Clothes', 'Transport', 'Others'];

  usersNames: string[] = [];
  expenseDetails: Expense = {
    expenseId: '',
    typeExpense: '',
    dateTime: new Date(),
    total: 0,
    paidBy: ''
  }

  constructor(private route: ActivatedRoute, private expensesService: ExpensesService, private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
this.route.paramMap.subscribe({
  next: (params) => {
    const id = params.get('expenseId');
    if(id){
        this.expensesService.getExpense(id)
        .subscribe({
          next: (response) =>
          this.expenseDetails = response
        })
    }
  }
})
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

  updateExpense() {
    this.expensesService.updateExpense(this.expenseDetails.expenseId, this.expenseDetails)
    .subscribe({
      next: (response) => {
        this.router.navigate(['expenses']); 
      }
    })
  }

  deleteExpense(id: string) {
    this.expensesService.deleteExpense(this.expenseDetails.expenseId)
    .subscribe({
      next: (response) => {
        this.router.navigate(['expenses']); 
      }
    })
  }

}
