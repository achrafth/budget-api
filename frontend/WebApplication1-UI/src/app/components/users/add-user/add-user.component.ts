import { Component, OnInit } from '@angular/core';
import { Route, Router, RouterEvent } from '@angular/router';
import { User } from 'src/app/Models/user.model';
import { UsersService } from 'src/app/Services/users/users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  addUserRequest: User = {
    id: '',
    email: '',
    name: '',
    lastName: '',
    budget: 0,
    dept: 0
  }
  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
  }

  addUser(){
    this.usersService.addUser(this.addUserRequest) 
    .subscribe({
      next: (response) => {
        this.router.navigate(['users']); 
      }
    })
  }

}
