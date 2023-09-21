import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { UsersService } from "src/app/shared/service/users.service";
import { ActivatedRoute } from "@angular/router";
import { Classroom } from "src/app/shared/models/users";
import { Registry } from "src/app/shared/models/users";
import { Prova } from "src/app/shared/models/users";

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
  classroomId!: FormGroup;
  role!: FormGroup;

  constructor(
    private usersService: UsersService,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {}

  ngOnInit() {

    this.registry = this.fb.group({
      name: new FormControl( null, Validators.required),
      surname: new FormControl( null, Validators.required),
      birth: new FormControl(null),
      gender: new FormControl( null, Validators.required),
      email: new FormControl( null, Validators.email),
      address: new FormControl( null ),
      telephone: new FormControl(
        null,
        Validators.pattern(
          /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
        )
      ),
    });
    this.user = this.fb.group({
      username: new FormControl( null, Validators.required),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
    this.classroomId = new FormGroup( null);
    this.role = new FormGroup( null, Validators.required);


    this.usersForm = this.fb.group({
      registry: this.registry,
      user: this.user,
      classroomId: this.classroomId,
      role: this.role,
    });



        // this.registry = this.fb.group({
        //   name: new FormControl("sjgfysjgfyj", Validators.required),
        //   surname: new FormControl("sjgfysjgfyj", Validators.required),
        //   birth: new FormControl("2007-02-08"),
        //   gender: new FormControl("Maschio", Validators.required),
        //   email: new FormControl("dsfsfdsffd@gmail.com", Validators.email),
        //   address: new FormControl("gyry6fytfyt"),
        //   telephone: new FormControl(
        //     "123-4567890",
        //     Validators.pattern(
        //       /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
        //     )
        //   ),
        // });
        // this.user = this.fb.group({
        //   username: new FormControl("giani", Validators.required),
        //   password: new FormControl("giacominopaneeino", [
        //     Validators.required,
        //     Validators.minLength(6),
        //   ]),
        // });
        // this.classroomId = new FormControl(
        //   "0ed3811a-0a5c-4ed0-b7db-53090199aa27"
        // );
        // this.role = new FormControl("teacher", Validators.required);



    // this.usersForm = new FormGroup({
    //   username: new FormControl(null, Validators.required),
    //   password: new FormControl(null, [
    //     Validators.required,
    //     Validators.minLength(6),
    //   ]),
    //   name: new FormControl(null, Validators.required),
    //   surname: new FormControl(null, Validators.required),
    //   birth: new FormControl(null),
    //   gender: new FormControl(null, Validators.required),
    //   email: new FormControl(null, Validators.email),
    //   address: new FormControl(null),
    //   telephone: new FormControl(
    //     null,
    //     Validators.pattern(
    //       /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
    //     )
    //   ),
    //   classroomId: new FormControl(null),
    //   roleName: new FormControl(null, Validators.required),
    // });


    // this.usersForm = this.builder.group({
    //   registry: this.builder.group({
    //     name: new FormControl(null, Validators.required),
    //     surname: new FormControl(null, Validators.required),
    //     gender: new FormControl(null, Validators.required),
    //     birth: new FormControl(null),
    //     email: new FormControl(null, Validators.email),
    //     address: new FormControl(null),
    //     telephone: new FormControl(
    //       null,
    //       Validators.pattern(
    //         /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
    //       )
    //     ),
    //   }),
    //   user: this.builder.group({
    //     username: new FormControl(null, Validators.required),
    //     password: new FormControl(null, [
    //       Validators.required,
    //       Validators.minLength(6),
    //     ]),
    //   }),
    //   classroomId: new FormControl(null),
    //   roleName: new FormControl(null, Validators.required),
    // });

    // per la creazione del wizard
    this.route.queryParams.subscribe((params) => {
      const mode = params["mode"];
      console.log(mode);

      if (mode === "edit") {
        // mi serve caricare i dati dell'utente selezionato
        this.idUser = this.route.snapshot.params["idUser"];
        this.getDataUser(this.idUser);
      } else {
        this.usersForm.reset();
      }
    });
    this.getClassroom();
  }

  onClickRole(role: string): void {
    this.roleValue = role;
    this.usersForm
      .get("classroomId")!
      .setValidators(this.getClassroomValidators());
    this.usersForm.get("classroomId")!.updateValueAndValidity();
    this.usersForm.get("role")!.setValue(this.role);
  }

  onClickGender(gender: string): void {
    this.gender = gender;
    this.usersForm.get("gender")!.setValue(this.gender);
  }

  // onClickClassroom(classId: string) {
  //   // this.classes.map((classroom) => {
  //   //   classroom.name = className;
  //     this.usersForm.value.classroomId!.setValue(classId);
  //   // });
  // }

  getClassroomValidators(): any {
    if (this.roleValue === "studente") {
      return Validators.required;
    }
  }

  showAlerts(formControlName: string): boolean {
    const formControl = this.usersForm.get(formControlName);
    return !!formControl && formControl.invalid;
  }

  onAddUser() {
    if (this.registry.valid && this.user.valid && this.classroomId.valid && this.role.valid && this.roleValue === "insegnante") {
      this.roleValue = "teacher";
      const dataUser = {
        registry: this.registry.value,
        user: this.user.value,
        classroomId: this.classroomId.value,
        role: this.role.value,
      };
      console.log(dataUser);
      this.usersService
        .addUser({
          registry: this.registry.value,
          user: this.user.value,
          classroomId: this.classroomId.value,
          role: this.role.value,
        })
        .subscribe({
          next: (res) => {
            console.log("ruolo", this.roleValue);

            console.log("tentativo", res);
          },
          error: (error) => {
            console.log(error);
          },
        });
    } else {
      if (
        this.registry.valid &&
        this.user.valid &&
        this.classroomId.valid &&
        this.role.valid &&
        this.usersForm.value.classroom !== null
      ) {
        this.roleValue = "student";
        const dataUser = {
          registry: this.registry.value,
          user: this.user.value,
          classroomId: this.classroomId.value,
          role: this.role.value,
        };
        console.log(dataUser);
        this.usersService
          .addUser({
            registry: this.registry.value,
            user: this.user.value,
            classroomId: this.classroomId.value,
            role: this.role.value,
          })
          .subscribe({
            next: (res) => {
              console.log(res);
              console.log("ruolo", this.roleValue);
              console.log(this.usersForm.value);
            },
            error: (error) => {
              console.log(error);
            },
          });
      }
    }
    console.log(this.usersForm.value);
  }

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
