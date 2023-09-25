import { Component } from '@angular/core';
import { Registry, Users, Prova } from 'src/app/shared/models/users';
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
  role: string = "";
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

  onClickRole(role: string): void {
    this.role = role;
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

    switch(this.role) {
      case 'student':
      role = 'Student';
      break;
      case 'teacher':
      role = 'Teacher';
      break;
    }
    console.log(this.text)

    this.usersService.getUsers(order, type, this.page, role, search).subscribe({
      next: (data: Registry[]) => {
        console.log(id)
       
        if(id) {
          this.orders = {
            name: 'asc',
            surname: 'asc',
            birth: 'asc',
            
            [id]: type
            
          };          
        }
        console.log("orders", this.orders);
        // id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
        // id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc"; 
        // id === 'birth' && this.orderBirth === "desc" ? this.orderBirth = "asc" : this.orderBirth = "desc";
        
        this.users = data;
      },
      error: (error) => {
        console.log(error);
      }
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

