import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UsersService } from 'src/app/shared/service/users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {

  usersForm!: FormGroup;
  role: string = "";
  gender: string = "";
  alert: boolean = false;

  constructor(private serviceUsers: UsersService) {}


  ngOnInit() {
    this.usersForm = new FormGroup({
      role: new FormControl(null, Validators.required),
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      name: new FormControl(null, Validators.required),
      surname: new FormControl(null, Validators.required),
      birth: new FormControl(null),
      gender: new FormControl(null, Validators.required),
      email: new FormControl(null, Validators.email),
      address: new FormControl(null),
      telephone: new FormControl(null, Validators.pattern(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/)),
      classroom: new FormControl(null),
    });   
  }

  onClickRole(role: string): void {
    this.role = role;
    this.usersForm.get('classroom')!.setValidators(this.getClassroomValidators());
    this.usersForm.get('classroom')!.updateValueAndValidity();
    this.usersForm.get('role')!.setValue(this.role);
  }

  onClickGender(gender: string): void {
    this.gender = gender;
    this.usersForm.get('gender')!.setValue(this.gender);
  }

  getClassroomValidators(): any {
    if (this.role === 'studente') {
      return Validators.required;
    }
  } 

  showAlerts(formControlName: string): boolean {
    const formControl = this.usersForm.get(formControlName);
    return !!formControl && formControl.invalid;
  }

  onAddUser() {
    console.log("onAddUser", this.role, this.usersForm.value)
    if(this.usersForm.valid && this.role === "insegnante") {
      this.serviceUsers.addTeacher(this.usersForm).subscribe({
        next: (res) => {
          console.log(res);
        },
        error: (error) => {
          console.log(error);
        }
      });
    } else {
      if(this.usersForm.valid && this.usersForm.value.classroom !== null) {
        this.serviceUsers.addStudent(this.usersForm).subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (error) => {
            console.log(error);
          }
        });
    }
    }
  }
  
}
