import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/Models/user.model';
import { UsersService } from 'src/app/Services/users/users.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  userDetails: User = {
    id: '',
    email: '',
    name: '',
    lastName: '',
    budget: 0,
    dept: 0
  }

  constructor(private route: ActivatedRoute, private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
this.route.paramMap.subscribe({
  next: (params) => {
    const id = params.get('id');
    if(id){
        this.usersService.getUser(id)
        .subscribe({
          next: (response) =>
          this.userDetails = response
        })
    }
  }
})
  }

  updateUser() {
    this.usersService.updateUser(this.userDetails.id, this.userDetails)
    .subscribe({
      next: (response) => {
        this.router.navigate(['users']); 
      }
    })
  }

  deleteUser(id: string) {
    this.usersService.deleteUser(this.userDetails.id)
    .subscribe({
      next: (response) => {
        this.router.navigate(['users']); 
      }
    })
  }

}
