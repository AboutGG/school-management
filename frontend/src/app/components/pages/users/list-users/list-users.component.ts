import { Component } from '@angular/core';
import { Registry, Users, Prova, ListResponse } from 'src/app/shared/models/users';
import { UsersService } from 'src/app/shared/service/users.service';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss']
})
export class ListUsersComponent {

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.page = 1;
    this.getData("Name", "asc", "name");
    
  }

  users: Registry[] = [];
  filter: string = "";
  action: string = "";
  id!: string;
  page: number = 1;
  text: string = "";
  
  orders: {
    name: 'asc' | 'desc',
    surname: 'asc' | 'desc',
    birth: 'asc' | 'desc'
  } = {
    name: 'asc',
    surname: 'asc',
    birth: 'asc'
  }

  onClickRole(): void {
    let boh ="";
    this.filter = boh;
    console.log(this.filter);
    
    this.getData('Name', this.orders.name)
  }

  onClickAction(action: string, id: string): void {
    this.action = action;
    this.id = id;
    console.log(this.id)
  }

  onClickPage(page: number) {
    this.page = page;
    console.log(this.page)
    this.getData('Name', this.orders.name)
  }
  

  getData(order: string, type: 'asc' | 'desc', id?: keyof typeof this.orders, search?: string): void {

    let role = "";
    search = this.text;

    switch (this.filter) {
      case "studenti":
        role = "student";
        break;
      case "insegnanti":
        role = "teacher";
        break;
    }
    console.log(this.filter);

    this.usersService
      .getUsers(order, type, this.page, this.filter, search)
      .subscribe({
        next: (res: ListResponse) => {
          console.log(id);

          if (id) {
            this.orders = {
              name: "asc",
              surname: "asc",
              birth: "asc",

              [id]: type,
            };
          }
          console.log("orders", this.orders);
          // id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
          // id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc";
          // id === 'birth' && this.orderBirth === "desc" ? this.orderBirth = "asc" : this.orderBirth = "desc";

          this.users = res.data;
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  dUser(id: string): void {
    console.log(id)
    this.usersService.deleteUser(id).subscribe({
      next: (res) => {
        this.page = 1; 
        this.getData("Name", "asc", "name");
        console.log(res);
      },
      error: (error) => {
        console.log('error',error);
      }
    });
  }

  saveSearch(text: string) {
    this.text = text;
    this.getData("Name", "asc", "name", this.text);
  }
}

