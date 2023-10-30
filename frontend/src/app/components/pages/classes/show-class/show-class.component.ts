import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Registry, Students, Teachers } from 'src/app/shared/models/users';
import { ClassDetails, Classroom, TeacherClassroom } from 'src/app/shared/models/classrooms';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { HttpParams } from '@angular/common/http';
import { Form, FormControl, FormGroup, Validators} from "@angular/forms";
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
  nameSurname!: string;
  promotion: boolean = true;
  // historyPromotionForm!: FormGroup;
  currentDate = new Date();
  promotionStartDate = new Date();
  promotionEndDate = new Date();



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
      promoted: new FormControl(true, Validators.required),
      nextClassroom: new FormControl(null, Validators.required),
    });

    // this.historyPromotionForm = new FormGroup({
    //   studentId: new FormControl(null),
    // });

    this.promotionStartDate.setMonth(5);
    this.promotionStartDate.setDate(1);

    this.promotionEndDate.setMonth(6);
    this.promotionEndDate.setDate(31);  

    this.getClassroom();
  }

  fetchClassDetails() {
    const params = new HttpParams().set("Order", this.order);
    this.classroomService.getSingleClassroom(this.classId, params).subscribe({
      next: (res: ListResponse<ClassDetails>) => {
        this.classDetails = res.data;
        console.log(this.classDetails.students);
        
        this.classDetails.students.map((student: Students) => {
          const userId = student.id;
          this.classroomService.getGrade(this.classId, userId!).subscribe({
            next: (res) => {
              student.finalGrade = res.finalGrade;
              student.fullName = res.fullName;
              // this.finalGrade = student.finalGrade;
              console.log(student.finalGrade);
              console.log(student.fullName);
            },
          });

          // console.log(student.id);
          // const dummy  = this.getFinalGrade(student.id!);
          // student.finalGrade = dummy.
        });
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

  getFinalGrade(idUser: string) {
    this.idUser = idUser;
    this.classroomService.getGrade(this.classId, this.idUser).subscribe({
      next: (res: Grade) => {
        this.studentGrade = res;
        // this.finalGrade = this.studentGrade.finalGrade

        this.graduationForm.patchValue({
          promoted: this.studentGrade.finalGrade >= 6 ? true : false,
          // promoted: this.finalGrade >= 6 ? true : false,
        });
        console.log("res", res);
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

  addGraduation() {
    this.classroomService
      .addStudentGraduation(
        this.graduationForm.value,
        this.classId,
        this.idUser
      )
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
  }
}



