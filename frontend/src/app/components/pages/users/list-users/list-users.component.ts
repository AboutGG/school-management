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
    this.getData("Name", "asc", "name");
    // this.getDataSurnameAsc();
    
  }

  users: Registry[] = [];

  role: string = "";
  action: string = "";
  orderName: string = "";
  orderSurname: string = "";
  // orderBirth: string = "";

  onClickRole(role: string): void {
    this.role = role;
  }

  onClickAction(action: string): void {
    this.action = action;
  }

  
  // getDataNameDesc(): void {
  //   this.usersService.getUsers("Name", "desc", 1).subscribe({
  //     next: (data: Registry[]) => {
        
  //       this.orderName = "asc";
  //       this.users = data;
  //     },
  //     error: (error) => {
  //       console.log(error);
  //     }
  //   });
  // }
  

  getData(type: string, order: string, id: string): void {
    this.usersService.getUsers(type, order, 1).subscribe({
      next: (data: Registry[]) => {
        
        id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
        id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc";       
        this.users = data;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  // getDataSurnameAsc(): void {
  //   this.usersService.getUsers("Surname", "asc", 1).subscribe({
  //     next: (data: Registry[]) => {
        
  //       this.orderSurname = "desc";
  //       this.users = data;
  //     },
  //     error: (error) => {
  //       console.log(error);
  //     }
  //   });
  // }

  // getDataSurnameDesc(): void {
  //   this.usersService.getUsers("Surname", "desc", 1).subscribe({
  //     next: (data: Registry[]) => {
        
  //       this.orderSurname = "asc";
  //       this.users = data;
  //     },
  //     error: (error) => {
  //       console.log(error);
  //     }
  //   });
  // }

  // getDataBirthAsc(): void {
  //   this.usersService.getUsers("Birth", "asc", 1).subscribe({
  //     next: (data: Registry[]) => {
        
  //       this.orderBirth = "desc";
  //       this.users = data;
  //     },
  //     error: (error) => {
  //       console.log(error);
  //     }
  //   });
  // }

  // getDataBirthDesc(): void {
  //   this.usersService.getUsers("Birth", "desc", 1).subscribe({
  //     next: (data: Registry[]) => {
        
  //       this.orderBirth = "asc";
  //       this.users = data;
  //     },
  //     error: (error) => {
  //       console.log(error);
  //     }
  //   });
  // }


}
