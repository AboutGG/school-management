import { Component } from '@angular/core';
import { Registry, Users } from 'src/app/shared/models/users';
import { UsersService } from 'src/app/shared/service/users.service';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss']
})
export class ListUsersComponent {

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.getData();
  }

  users: Registry[] = [];

  role: string = "";
  action: string = "";

  onClickRole(role: string): void {
    this.role = role;
  }

  onClickAction(action: string): void {
    this.action = action;
  }

  getData(): void {
    this.usersService.getUsers("Name", 1, 5).subscribe({
      next: (data: Registry[]) => {
        this.users = data;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
