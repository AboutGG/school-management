import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { UsersService } from "src/app/shared/service/users.service";
import { ActivatedRoute } from "@angular/router";
import { Classroom } from "src/app/shared/models/users";

@Component({
  selector: "app-add-user",
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.scss"],
})
export class AddUserComponent implements OnInit {
  usersForm!: FormGroup;
  role: string = "";
  gender: string = "";
  alert: boolean = false;
  classes: Classroom[] = [];

  constructor(
    private serviceUsers: UsersService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.usersForm = new FormGroup({
      role: new FormControl(null, Validators.required),
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
      name: new FormControl(null, Validators.required),
      surname: new FormControl(null, Validators.required),
      birth: new FormControl(null),
      gender: new FormControl(null, Validators.required),
      email: new FormControl(null, Validators.email),
      address: new FormControl(null),
      telephone: new FormControl(
        null,
        Validators.pattern(
          /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
        )
      ),
      classroom: new FormControl(null),
    });

    // per la creazione del wizard
    this.route.queryParams.subscribe((params) => {
      const mode = params["mode"];
      if (mode === "edit") {
        // mi serve caricare i dati dell'utente selezionato
        this.getDataUser(); // devo creare la funzione che si prende i dati dell'utente e li torna
      } else {
        this.usersForm.reset();
      }
    });
    this.getClassroom();
  }

  onClickRole(role: string): void {
    this.role = role;
    this.usersForm
      .get("classroom")!
      .setValidators(this.getClassroomValidators());
    this.usersForm.get("classroom")!.updateValueAndValidity();
    this.usersForm.get("role")!.setValue(this.role);
  }

  onClickGender(gender: string): void {
    this.gender = gender;
    this.usersForm.get("gender")!.setValue(this.gender);
  }

  onClickClassroom(className: string) {
    this.classes.map(classroom => {
      classroom.name_classroom = className 
      this.usersForm.get(className)!.setValue(classroom.name_classroom)
    })
  }


  getClassroomValidators(): any {
    if (this.role === "studente") {
      return Validators.required;
    }
  }

  showAlerts(formControlName: string): boolean {
    const formControl = this.usersForm.get(formControlName);
    return !!formControl && formControl.invalid;
  }


  onAddUser() {
    if (this.usersForm.valid && this.role === "insegnante") {this.role = "teacher"
      this.serviceUsers.addUser(this.usersForm.value, this.role).subscribe({
        next: (res) => {
          console.log("ruolo", this.role);
          
          console.log("tentativo", res);
        },
        error: (error) => {
          console.log(error);
        },
      });
    } else {
      if (this.usersForm.valid && this.usersForm.value.classroom !== null) { this.role = "student"
        this.serviceUsers.addUser(this.usersForm.value, this.role).subscribe({
          next: (res) => {
            console.log(res);
                      console.log("ruolo", this.role);

          },
          error: (error) => {
            console.log(error);
          },
        });
      }
    }
  }
  
  // onAddUser() {
  //   console.log("onAddUser", this.role, this.usersForm.value);
  //   if (this.usersForm.valid && this.role === "insegnante") {
  //     this.serviceUsers.addTeacher(this.usersForm.value, this.role).subscribe({
  //       next: (res) => {
  //         console.log("tentativo", res);
  //       },
  //       error: (error) => {
  //         console.log(error);
  //       },
  //     });
  //   } else {
  //     if (this.usersForm.valid && this.usersForm.value.classroom !== null) {
  //       this.serviceUsers.addStudent(this.usersForm.value, this.role).subscribe({
  //         next: (res) => {
  //           console.log(res);
  //         },
  //         error: (error) => {
  //           console.log(error);
  //         },
  //       });
  //     }
  //   }
  // }

  getDataUser() {
    this.serviceUsers.getUsers;
  }

  getClassroom() {
    this.serviceUsers.getClassroom().subscribe((data) => {
      this.classes = data;
      console.log(this.classes);
    });
  }
}
