import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Registry, Students, Teachers } from 'src/app/shared/models/users';
import { ClassDetails, Classroom, TeacherClassroom } from 'src/app/shared/models/classrooms';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { HttpParams } from '@angular/common/http';
import { Form, FormControl, FormGroup, Validators } from "@angular/forms";
import { Grade, StudentGraduation } from 'src/app/shared/models/studentgraduation';

@Component({
  selector: "app-show-class",
  templateUrl: "./show-class.component.html",
  styleUrls: ["./show-class.component.scss"],
})
export class ShowClassComponent {
  classId!: string;
  classDetails!: ClassDetails;
  isTeacher: boolean = false; //memorizza se l'utente è insegnante
  order: string = "Registry.Surname";

  userGraduation!: StudentGraduation;
  graduationForm!: FormGroup;
  classes!: Classroom[];
  studentGrade!: Grade;
  gradeForm!: FormGroup;
  idUser!: string;
  fullName!: string;
  finalGrade!: number;
  promotion: boolean = true;


  constructor(
    private classroomService: ClassroomService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.classId = params["id"];
    });
    this.fetchClassDetails();
    this.isTeacher = this.authService.isTeacher();
    console.log();
    

    this.graduationForm = new FormGroup({
      scholasticBehavior: new FormControl(null, Validators.required),
      promoted: new FormControl(null, Validators.required),
      nextClassroom: new FormControl(null, Validators.required),
    });
  
    this.getClassroom();
  }

  fetchClassDetails() {
    const params = new HttpParams().set("Order", this.order);
    this.classroomService.getSingleClassroom(this.classId, params).subscribe({
      next: (res: ListResponse<ClassDetails>) => {
        this.classDetails = res.data;
        this.classDetails.students.map((student) => {})
        console.log(res.data);
      },
      error: (err) => {
        console.log("error", err);
      },
    });
  }

  navigateToTeachersClasses() {
    this.router.navigate(["teachers/classes"]);
  }

  addGraduation() {
    this.classroomService.addStudentGraduation(this.graduationForm.value).subscribe({
        next: (res) => {},
        error: (error) => {
          console.log(error);
        },
      });
  }

  getFinalGrade(idUser: string) {
    this.idUser = idUser;
    this.classroomService.getGrade(this.classId, this.idUser).subscribe({
      next: (res: Grade) => {
        this.studentGrade = res;
        console.log('res', res)  
      },
    });
  }



  getClassroom() {
    this.classroomService.getClassroom().subscribe({
      next: (res) => {
        this.classes = res;
        console.log("prova", this.classes);
      },
    });
  }
}
