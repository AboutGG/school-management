import { Component } from '@angular/core';
import { Registry } from 'src/app/shared/models/users';
import { UsersService } from 'src/app/shared/service/users.service';


@Component({
  selector: 'app-examslist',
  templateUrl: './examslist.component.html',
  styleUrls: ['./examslist.component.scss']
})
export class ExamslistComponent {
  constructor(private usersService: UsersService) {}
  

  users: Registry[] = [];
  matter: string = "";
  action: string = "";
  orders = {
    name: 'asc',
    surname: 'asc',
    birth: 'asc'
  }

  onClickMatter(matter: string): void {
    this.matter = matter;
  }

  onClickAction(action: string): void {
    this.action = action;
  }

  
  

  getData(order: string, type: 'asc' | 'desc', id: keyof typeof this.orders): void {
    this.usersService.getUsers().subscribe({
      next: (data: Registry[]) => {
        this.orders = {
          name: 'asc',
          surname: 'asc',
          birth: 'asc',
          [id]: type
        };
        // id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
        // id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc"; 
        // id === 'birth' && this.orderBirth === "desc" ? this.orderBirth = "asc" : this.orderBirth = "desc";

        this.users = data;
  // resetAllDropdowns() {
  //   [this.formClassrooms.reset({classrooms: ""})] && [this.formSubjects.reset({subjects: ""})]
  //   this.getTeacherExams()
  // }
  
      }})
    }}