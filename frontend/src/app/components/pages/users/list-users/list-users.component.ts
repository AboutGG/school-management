import { Component } from '@angular/core';
import { Registry, Users } from 'src/app/shared/models/users';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss']
})
export class ListUsersComponent {
  users: Registry[] = [{
    name: "Giorgio",
    surname: "Catania",
    gender: "Uomo",
    birth: "26-03-1991"
  },{
    name: "Andrea",
    surname: "Palermo",
    gender: "Uomo",
    birth: "03-12-2001"
  },
  {
    name: "Gigi",
    surname: "Sedia",
    gender: "Uomo",
    birth: "12-05-1986"
  },
  {
    name: "Angela",
    surname: "Bianco",
    gender: "Femmina",
    birth: "07-02-1995"
  },
  {
    name: "Anna",
    surname: "Lina",
    gender: "Other",
    birth: "20-08-1991"
  },
  {
    name: "Salvo",
    surname: "Stanco",
    gender: "Uomo",
    birth: "13-10-1999"
  },
  {
    name: "Maria",
    surname: "Quoto",
    gender: "Femmina",
    birth: "02-11-1961"
  },
  {
    name: "Mia",
    surname: "Amica",
    gender: "Femmina",
    birth: "24-09-1971"
  },
  {
    name: "Tino",
    surname: "Gaetano",
    gender: "Other",
    birth: "14-12-2003"
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
