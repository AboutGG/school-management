import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.scss']
})
export class UsersPageComponent implements OnInit {

  usersForm!: FormGroup;

  constructor() {}

  ngOnInit() {
    this.usersForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      birth: new FormControl(''),
      gender: new FormControl('', Validators.required),
      email: new FormControl('', Validators.email),
      address: new FormControl(''),
      telephone: new FormControl('', Validators.pattern("^[0-9]")),
      classroom: new FormControl('', Validators.required),

    })
  }

}
