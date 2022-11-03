import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddUserComponent } from './components/users/add-user/add-user.component';
import { UpdateUserComponent } from './components/users/update-user/update-user.component';
import { UsersListComponent } from './components/users/users-list/users-list.component';
import { AddExpenseComponent } from './components/expenses/add-expense/add-expense.component';
import { UpdateExpenseComponent } from './components/expenses/update-expense/update-expense.component';
import { ExpensesListComponent } from './components/expenses/expenses-list/expenses-list.component';

const routes: Routes = [
  {
    path: 'users',
    component: UsersListComponent
  },
  {
    path: 'users/add',
    component: AddUserComponent
  },
  {
    path: 'users/update/:id',
    component: UpdateUserComponent
  },
  {
    path: 'expenses',
    component: ExpensesListComponent
  },
  {
    path: 'expenses/add',
    component: AddExpenseComponent
  },
  {
    path: 'expenses/update/:expenseId',
    component: UpdateExpenseComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
