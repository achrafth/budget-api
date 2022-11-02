import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsersListComponent } from './components/users/users-list/users-list.component';
import { AddUserComponent } from './components/users/add-user/add-user.component';
import { UpdateUserComponent } from './components/users/update-user/update-user.component';
import { ExpensesListComponent } from './components/expenses/expenses-list/expenses-list.component';
import { AddExpenseComponent } from './components/expenses/add-expense/add-expense.component';
import { UpdateExpenseComponent } from './components/expenses/update-expense/update-expense.component';
import { NgSelectModule } from "@ng-select/ng-select";

@NgModule({
  declarations: [
    AppComponent,
    UsersListComponent,
    AddUserComponent,
    UpdateUserComponent,
    ExpensesListComponent,
    AddExpenseComponent,
    UpdateExpenseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
 