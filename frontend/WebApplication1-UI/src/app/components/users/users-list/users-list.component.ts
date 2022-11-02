import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/user.model';
import { UsersService } from 'src/app/Services/users/users.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit {

  users: User[] = [];
  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
 this.usersService.getAllUsers().subscribe({
      next: (users) => {
          this.users = users;
          console.log(users)
      },
      error: (response) => {
    console.log(response);
      }
    });
  }
}
