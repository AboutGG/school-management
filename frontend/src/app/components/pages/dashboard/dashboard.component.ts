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
import { ActivatedRoute } from "@angular/router";

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

  examsTeachers!: TeacherExam[];
  examsStudents!: StudentExam[];
  order: string = 'Date';
  orderPdf: string = 'UploadDate'
  editForm!: FormGroup;
  pdf!: PdfCirculars;
  pdfs!: PdfCirculars[];
  circularId!: string;
  currentDate = new Date();
  day = this.currentDate.getDate();
  month = this.currentDate.getMonth() + 1;
  year = this.currentDate.getFullYear();
  today = this.year + "-" + this.month + "-" + this.day;
  itemsPerPage = 3;


  constructor(
    private commonService: CommonService, 
    private classroomService: ClassroomService,
    private teacherService: TeacherService,
    private examsService: ExamsService,
    private fb: NonNullableFormBuilder,
    private route: ActivatedRoute,

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
    .set('OrderType', 'desc')
    this.commonService.getCirculars(params).subscribe({
      next: (res: ListResponse<PdfCirculars[]>) => {
        this.pdfs = res.data;
      },
      error:(err) => {
        console.log("error",err);
      }

    })
  }

  getCircularsById(pdf: PdfCirculars){
    this.commonService.getCircularsById(pdf.id).subscribe( blob => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url; 
      a.download = pdf.object;
      a.style.display = 'none';
      document.body.appendChild(a); 
      a.click(); 
      window.URL.revokeObjectURL(url);
    });
  }


  getCount() {
    this.commonService.getCount().subscribe((res) => {
      this.count = res;
    })
  }

  getExams(isTeacher: boolean) {
    const params = new HttpParams()
    .set('Order', this.order)
    
    if (isTeacher) {
      this.examsService.getTeacherExams(params).subscribe({
        next: (res: ListResponse<TeacherExam[]>) => {

          this.examsTeachers = res.data
            .filter((item, index) => item.date > this.today)
            .filter((_, index) => index < this.itemsPerPage);

        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    } else {
      this.examsService.getStudentExams(params).subscribe({
        next: (res: ListResponse<StudentExam[]>) => {
          
          this.examsStudents = res.data
            .filter((item, index) => item.date > this.today)
            .filter((_, index) => index < this.itemsPerPage);
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
