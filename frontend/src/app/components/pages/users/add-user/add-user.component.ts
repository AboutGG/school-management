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
      role: new FormControl('', Validators.required),
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      birth: new FormControl(''),
      gender: new FormControl('', Validators.required),
      email: new FormControl('', Validators.email),
      address: new FormControl(''),
      telephone: new FormControl(null, Validators.pattern("^[0-9]")),
      classroom: new FormControl(''),
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
    this.serviceUsers.addUser(this.usersForm.value).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
