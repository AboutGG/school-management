import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { UsersService } from "src/app/shared/service/users.service";
import { ActivatedRoute } from "@angular/router";
import { Classroom } from "src/app/shared/models/users";
import { Registry } from "src/app/shared/models/users";
import { Users } from "src/app/shared/models/users";

@Component({
  selector: "app-add-user",
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.scss"],
})
export class AddUserComponent implements OnInit {
  usersForm!: FormGroup;
  roleValue: string = "";
  gender: string = "";
  alert: boolean = false;
  classes: Classroom[] = [];
  idUser!: string;
  classroom!: string;

  registry!: FormGroup;
  user!: FormGroup;
  classroomId!: FormControl;
  role!: FormControl;
  

  constructor(
    private usersService: UsersService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {

      this.registry = new FormGroup({
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
      }),
      this.user = new FormGroup({
        username: new FormControl(null, Validators.required),
        password: new FormControl(null, [
          Validators.required,
          Validators.minLength(6),
        ]),
      });
      this.classroomId = new FormControl(null),
      this.role = new FormControl(null, Validators.required),
    

//form con dati moccati
    // this.registry = new FormGroup({
    //   name: new FormControl("serio", Validators.required),
    //   surname: new FormControl("giorno", Validators.required),
    //   birth: new FormControl("2001-01-08"),
    //   gender: new FormControl("Maschio", Validators.required),
    //   email: new FormControl("boh@gmail.com", Validators.email),
    //   address: new FormControl("hhhhht"),
    //   telephone: new FormControl(
    //     "111-4568890",
    //     Validators.pattern(
    //       /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
    //     )
    //   ),
    // });
    // this.user = new FormGroup({
    //   username: new FormControl("sgiii", Validators.required),
    //   password: new FormControl("noooooooooo888", [
    //     Validators.required,
    //     Validators.minLength(6),
    //   ]),
    // });
    // this.classroomId = new FormControl("0ed3811a-0a5c-4ed0-b7db-53090199aa27");
    // this.role = new FormControl("student", Validators.required);

  //form con 4 form prova con domenico
    // this.usersForm = new FormGroup({
    //   registry: new FormGroup({
    //     name: new FormControl(null, Validators.required),
    //     surname: new FormControl(null, Validators.required),
    //     birth: new FormControl(null),
    //     gender: new FormControl(null, Validators.required),
    //     email: new FormControl(null, Validators.email),
    //     address: new FormControl(null),
    //     telephone: new FormControl(
    //       null,
    //       Validators.pattern(
    //         /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
    //       )
    //     ),
    //   }),
    //   user: new FormGroup({
    //     username: new FormControl(null, Validators.required),
    //     password: new FormControl(null, [
    //       Validators.required,
    //       Validators.minLength(6),
    //     ]),
    //   }),
    //   classroomId: new FormControl(null),
    //   role: new FormControl(null, Validators.required),
    // });

    this.usersForm = new FormGroup({
      registry: this.registry,
      user: this.user,
      classroomId: this.classroomId,
      role: this.role,
    });


    // per la creazione del wizard
    // this.route.queryParams.subscribe((params) => {
    //   const mode = params["mode"];
    //   console.log(mode);

    //   if (mode === "edit") {
    //     // mi serve caricare i dati dell'utente selezionato
    //     this.idUser = this.route.snapshot.params["idUser"];
    //     this.getDataUser(this.idUser);
    //   } else {
    //     this.usersForm.reset();
    //   }
    // });
    this.getClassroom();
  }

  onClickRole(event: any): void {

    this.roleValue = event.target.value;
    // this.roleValue = this.usersForm.get("role")?.value;
    
    // console.log(this.usersForm.value.role);
    console.log(this.roleValue);
    this.usersForm
      .get("classroomId")!
      .setValidators(this.getClassroomValidators());
    this.usersForm.get("classroomId")!.updateValueAndValidity();
    this.usersForm.get("role")!.setValue(this.roleValue);
  }

  onClickGender(): void {
    this.gender = this.usersForm.get("gender")?.value;
    this.usersForm.get("gender")!.setValue(this.gender);
  }

  // onClickClassroom(classId: string) {
  //   // this.classes.map((classroom) => {
  //   //   classroom.name = className;
  //     this.usersForm.value.classroomId!.setValue(classId);
  //   // });
  // }

  getClassroomValidators(): any {
    if (this.roleValue === 'student') {
      return Validators.required;
    }
  }

  showAlerts(formControlName: string): boolean {
    const formControl = this.usersForm.get(formControlName);
    return !!formControl && formControl.invalid;
  }

  onAddUser() {
      this.usersService.addUser(this.usersForm.value).subscribe({
        next: (res) => {
          console.log("ruolo", this.roleValue);

          console.log("tentativo", res);
        },
        error: (error) => {
          console.log(error);
        },
      });
    
    console.log(this.usersForm.value);
  }

  // onAddUser() {
  //   if (this.usersForm.valid && this.roleValue === "teacher") {
  //     this.roleValue = "teacher";
  //     this.usersService.addUser(this.usersForm.value).subscribe({
  //       next: (res) => {
  //         console.log("ruolo", this.roleValue);

  //         console.log("tentativo", res);
  //       },
  //       error: (error) => {
  //         console.log(error);
  //       },
  //     });
  //   } else {
  //     if (this.usersForm.valid && this.usersForm.value.classroom !== null) {
  //       this.roleValue = "student";
  //       this.usersService.addUser(this.usersForm.value).subscribe({
  //         next: (res) => {
  //           console.log(res);
  //           console.log("ruolo", this.roleValue);
  //           console.log(this.usersForm.value);
  //         },
  //         error: (error) => {
  //           console.log(error);
  //         },
  //       });
  //     }
  //   }
  //   console.log(this.usersForm.value);
  // }

  selectClassroom(classroom: string) {
    this.usersForm.value.classroomId!.setValue(classroom);
  }

  // onAddUser() {
  //   console.log("onAddUser", this.role, this.usersForm.value);
  //   if (this.usersForm.valid && this.role === "insegnante") {
  //     this.usersService.addTeacher(this.usersForm.value, this.role).subscribe({
  //       next: (res) => {
  //         console.log("tentativo", res);
  //       },
  //       error: (error) => {
  //         console.log(error);
  //       },
  //     });
  //   } else {
  //     if (this.usersForm.valid && this.usersForm.value.classroom !== null) {
  //       this.usersService.addStudent(this.usersForm.value, this.role).subscribe({
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

  getDataUser(idUser: string) {
    this.usersService.getDetailsUser(idUser).subscribe((userData: Registry) => {
      this.usersForm.patchValue({
        name: userData.name,
        surname: userData.surname,
      });
    });
  }

  // getDataUser() {
  //   const idUser = this.route.snapshot.params["idUser"];
  //   console.log(idUser);
  //   this.usersService.getDetailsUser(idUser).subscribe((res: Registry) => {
  //     console.log(res);
  //     this.getDataUser;
  //   });
  // }

  getClassroom() {
    this.usersService.getClassroom().subscribe((data) => {
      this.classes = data;
      console.log("prova", this.classes);
    });
  }
}
