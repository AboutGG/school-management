import { Component } from '@angular/core';
import { Registry, Users } from 'src/app/shared/models/users';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss']
})
export class ListUsersComponent {
  users: Registry[] = [{
    name: "Giordana",
    surname: "Pistorio",
    gender: "Femmina",
    birth: "15-09-1996"
  }];

  role: string = "";
  action: string = "";

  onClickRole(role: string): void {
    this.role = role;
  }

  onClickAction(action: string): void {
    this.action = action;
  }
}
