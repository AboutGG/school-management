import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { PdfCirculars } from "src/app/shared/models/pdf";
import { StudentExam } from "src/app/shared/models/studentexam";
import { TeacherExam } from "src/app/shared/models/teacherexam";
import { TypeCount } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { ClassroomService } from "src/app/shared/service/classroom.service";
import { CommonService } from "src/app/shared/service/common.service";
import { ExamsService } from "src/app/shared/service/exams.service";
import { TeacherService } from "src/app/shared/service/teacher.service";
import { UsersService } from "src/app/shared/service/users.service";
import { ListResponse } from 'src/app/shared/models/listresponse';

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {

  count: TypeCount = {
    Users: 0,
    Students: 0,
    Teachers:0,
    Classrooms:0
  }
  isTeacher = this.authService.isTeacher();

  pagella = {
    title: "Pagella 1° Quadrimestre", 
    img: "assets/dashboard/logoCircolari.jpg"
  }

  // pdfs = [
  //   {
  //     title: "Nuovo ordinamento scolastico",
  //     data: "10/10/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  //   {
  //     title: "Fine anno scolastico",
  //     data: "01/06/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  //   {
  //     title: "Programma esami di stato",
  //     data: "16/05/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  //   {
  //     title: "Convocazione consiglio d'istituto",
  //     data: "28/04/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  //   {
  //     title: "Assemblea d'istituto",
  //     data: "10/04/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"

  //   },
  //   {
  //     title: "Programma Carnevale a scuola",
  //     data: "03/02/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  //   {
  //     title: "Incontro scuola famiglia",
  //     data: "12/01/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  //   {
  //     title: "Circolare di fine anno solare",
  //     data: "21/12/2023",
  //     img: "assets/dashboard/logoCircolari.jpg"
  //   },
  // ]
  examsTeachers!: TeacherExam[];
  examsStudents!: StudentExam[];
  itemsPerPage: number = 3;
  order: string = 'Date';
  orderPdf: string = 'UploadDate'
  editForm!: FormGroup;
  pdf!: PdfCirculars;
  pdfs!: PdfCirculars[];


  constructor(
    private commonService: CommonService, 
    private classroomService: ClassroomService,
    private teacherService: TeacherService,
    private examsService: ExamsService,
    private fb: NonNullableFormBuilder,

    private authService: AuthService ){ 

      this.editForm =this.fb.group({
      circularNumber: new FormControl (null, Validators.required),
      uploadDate: new FormControl(null, Validators.required),
      location: new FormControl(null, Validators.required),
      object: new FormControl(null, Validators.required),
      header: new FormControl(null, Validators.required),
      body: new FormControl(null, Validators.required),
      sign: new FormControl(null, Validators.required),
    })}

  ngOnInit(): void {
    this.getCount()
    this.getExams(this.isTeacher);
    this.getCirculars();
    this.getCircularsById();
   
    //this.getClassroomCount()
  }

  addCircular() {
    const data=this.editForm.value
    console.log(data);
    this.commonService.addCirculars(data).subscribe((res) => {
      this.pdf = res;
      console.log(data);
  })
  }

  getCirculars() { 
    const params = new HttpParams()
    .set('Order', this.orderPdf)
    this.commonService.getCirculars(params).subscribe({
      next: (res: ListResponse<PdfCirculars[]>) => {
        this.pdfs = res.data;
        console.log(this.pdfs);
        
      },
      error:(err) => {
        console.log("error",err);
      }

    })
  }

  getCircularsById(){

  }

  getCount() {
    this.commonService.getCount().subscribe((res) => {
      this.count = res;
    })
  }

  getExams(isTeacher: boolean) {
    const params = new HttpParams()
    .set('Order', this.order)
    .set('ItemsPerPage', this.itemsPerPage)

    if (isTeacher) {
      this.examsService.getTeacherExams(params).subscribe({
        next: (res: ListResponse<TeacherExam[]>) => {
          this.examsTeachers = res.data;
        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    } else {
      this.examsService.getStudentExams(params).subscribe({
        next: (res: ListResponse<StudentExam[]>) => {
          this.examsStudents = res.data;
        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    }
  }
  

  // getClassroomCount() {
  //   this.classroomService.getDataClassroom().subscribe(({total}) => {
  //     this.count.Classrooms = total;
  //     console.log("totale classi", total);
  //   });
  // }
}
