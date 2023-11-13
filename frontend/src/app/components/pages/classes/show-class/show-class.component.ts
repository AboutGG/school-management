import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Students } from 'src/app/shared/models/users';
import { ClassDetails, Classroom } from 'src/app/shared/models/classrooms';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { HttpParams } from '@angular/common/http';
import { FormControl, FormGroup, Validators} from "@angular/forms";
import { Grade, StudentGraduation } from 'src/app/shared/models/studentgraduation';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: "app-show-class",
  templateUrl: "./show-class.component.html",
  styleUrls: ["./show-class.component.scss"],
})
export class ShowClassComponent {
  classId!: string;
  classDetails!: ClassDetails;
  isTeacher: boolean = false;
  order: string = "Registry.Surname";

  userGraduation!: StudentGraduation;
  graduationForm!: FormGroup;
  classes!: Classroom[];
  studentGrade!: Grade;
  gradeForm!: FormGroup;
  idUser!: string;
  fullName!: string;
  finalGrade!: number;
  nameSurname!: string;
  promotion: boolean = true;
  currentDate = new Date();
  promotionStartDate = new Date();
  promotionEndDate = new Date();

  currentYear!: boolean;
  schoolYear!: string;

  unsubscribe$: Subject<boolean> = new Subject<boolean>();

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
        this.currentYear = true;

    this.fetchClassDetails();
    this.isTeacher = this.authService.isTeacher();

    this.graduationForm = new FormGroup({
      scholasticBehavior: new FormControl(null, Validators.required),
      promoted: new FormControl(true, Validators.required),
      nextClassroom: new FormControl(null, Validators.required),
    });

    this.promotionStartDate.setMonth(5);
    this.promotionStartDate.setDate(1);
    this.promotionEndDate.setMonth(6);
    this.promotionEndDate.setDate(31);

    this.getClassroom();
  }

  fetchClassDetails() {
    const params = new HttpParams()
      .set("Order", this.order)
      .set("isCurrentYear", this.currentYear);
    this.classroomService
      .getSingleClassroom(this.classId, params)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (res: ListResponse<ClassDetails>) => {
          this.classDetails = res.data;
          console.log(this.classDetails.students);
    console.log(this.currentYear);

          this.classDetails.students.map((student: Students) => {
            const userId = student.id;
            this.classroomService
              .getGrade(this.classId, userId!)
              .pipe(takeUntil(this.unsubscribe$))
              .subscribe({
                next: (res) => {
                  student.finalGrade = res.finalGrade;
                  student.fullName = res.fullName;
                  console.log(student.finalGrade);
                  console.log(student.fullName);
                },
              });
          });
          console.log(res.data);
        },
        error: (err) => {
          console.log("error", err);
        },
      });
  }

  isCurrentYear() {
    this.fetchClassDetails();
    console.log(this.currentYear);
    
  }

  navigateToTeachersClasses() {
    this.router.navigate(["teachers/classes"]);
  }

  getFinalGrade(idUser: string) {
    this.idUser = idUser;
    this.classroomService
      .getGrade(this.classId, this.idUser)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (res: Grade) => {
          this.studentGrade = res;
          this.graduationForm.patchValue({
            promoted: this.studentGrade.finalGrade >= 6 ? true : false,
          });
          console.log("res", res);
        },
      });
  }

  getClassroom() {
    this.classroomService
      .getClassroom()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (res) => {
          this.classes = res;
          console.log("prova", this.classes);
        },
      });
  }

  addGraduation() {
    this.classroomService
      .addStudentGraduation(
        this.graduationForm.value,
        this.classId,
        this.idUser
      )
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (res) => {
          console.log("tentativo", this.graduationForm.value);
          alert("promozione inserita con successo");
        },
        error: (error) => {
          console.log(error);
          console.log("tentativo errore", this.graduationForm.value);
        },
      });
    console.log(this.graduationForm.value);
    this.fetchClassDetails();
    
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }
}



