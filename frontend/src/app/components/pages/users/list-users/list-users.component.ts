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
    this.page = 1;
    this.getData("Name", "asc", "name");
    
  }

  users: Registry[] = [];
  role: string = "";
  action: string = "";
  id!: string;
  page: number = 1;
  
  orders:{
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
  }

  onClickPage(page: number) {
    this.page = page;
    console.log(this.page)
    this.getData('Name', this.orders.name)
  }
  

  getData(order: string, type: 'asc' | 'desc', id?: keyof typeof this.orders): void {

    let role = "";

    switch(this.role) {
      case 'studenti':
      role = 'Student';
      break;
      case 'insegnanti':
      role = 'Teacher';
      break;
    }

    this.usersService.getUsers(order, type, this.page, role).subscribe({
      next: (data: Registry[]) => {
       
        if(id) {
          this.orders = {
            name: 'asc',
            surname: 'asc',
            birth: 'asc',
            
            [id]: type
          };
          
        }
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
}

