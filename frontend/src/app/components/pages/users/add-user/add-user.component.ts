import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { UsersService } from "src/app/shared/service/users.service";
import { ClassroomService } from "src/app/shared/service/classroom.service";
import { ActivatedRoute, Router } from "@angular/router";
import { Classroom } from "src/app/shared/models/classrooms";
import { Registry } from "src/app/shared/models/users";
import { Location } from "@angular/common";
import { Subject, takeUntil } from "rxjs";

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
  classes!: Classroom[];
  idUser!: string;
  classroom!: string;

  registry!: FormGroup;
  user!: FormGroup;
  classroomId!: FormControl;
  role!: FormControl;

  id!: string;
  details!: Registry

  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  constructor(
    private usersService: UsersService,
    private classroomService: ClassroomService,
    private route: ActivatedRoute,
    private location: Location,
  ) {}

  ngOnInit() {
    this.id = this.route.snapshot.params["id"];
    console.log(this.id);
    if (this.id){
    this.usersService.getDetailsUser(this.id).pipe(takeUntil(this.unsubscribe$)).subscribe((res: Registry) => { 
      console.log(res)
      this.details = res;
        this.editUser();
    })
  };
    
    this.registry = new FormGroup({
      name: new FormControl(null,[ Validators.required, Validators.minLength(3) ]),
      surname: new FormControl(null,[ Validators.required, Validators.minLength(3)]),
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
        username: new FormControl(null,[ Validators.required, Validators.minLength(3)]),
        password: new FormControl(null, [
          Validators.required,
          Validators.minLength(6),
        ]),
      });
    this.classroomId = new FormControl(null),
      this.role = new FormControl(null, Validators.required),
      
      this.usersForm = new FormGroup({
        registry: this.registry,
        user: this.user,
        classroomId: this.classroomId,
        role: this.role,
      });

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

  getClassroomValidators(): any {
    if (this.roleValue === "student") {
      return Validators.required;
    }
  }

  showAlerts(formControlName: string): boolean {
    const formControl = this.usersForm.get(formControlName);
    return !!formControl && formControl.invalid;
  }

  onAddUser() {
    if (this.id) {
      console.log();
      this.usersService.editUser(this.registry.value, this.id).subscribe()
      console.log(this.registry.value);
    }else {
      this.usersService.addUser(this.usersForm.value).subscribe({
        next: (res) => {
          console.log("ruolo", this.roleValue);

          console.log("tentativo", res);
        },
        error: (error) => {
         
          console.log(error);
        },
      });
    }
    console.log(this.usersForm.value);
  }

  selectClassroom(classroom: string) {
    this.usersForm.value.classroomId!.setValue(classroom);
  }

  getDataUser(idUser: string) {
    this.usersService.getDetailsUser(idUser).pipe(takeUntil(this.unsubscribe$)).subscribe((userData: Registry) => {
      this.usersForm.patchValue({
        name: userData.name,
        surname: userData.surname,
      });
    });
  }

  editUser() {
    this.registry.patchValue({
      name: this.details.name,
      surname: this.details.surname,
      birth: this.details.birth,
      gender: this.details.gender,
      email: this.details.email,
      address: this.details.address,
      telephone: this.details.telephone
    })
  }

  getClassroom() {
    this.classroomService.getClassroom().pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res) => {
        this.classes = res;
        console.log("prova", this.classes);
      },
    });
  }

  navigateTo() {
    this.location.back();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete(); 
  }
}
