import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { PdfCirculars } from "src/app/shared/models/pdf";
import { StudentExam } from "src/app/shared/models/studentexam";
import { TeacherExam } from "src/app/shared/models/teacherexam";
import { TypeCount, UsersMe } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { ClassroomService } from "src/app/shared/service/classroom.service";
import { CommonService } from "src/app/shared/service/common.service";
import { ExamsService } from "src/app/shared/service/exams.service";
import { TeacherService } from "src/app/shared/service/teacher.service";
import { UsersService } from "src/app/shared/service/users.service";
import { ListResponse } from 'src/app/shared/models/listresponse';
import { ActivatedRoute } from "@angular/router";
import { StudentService } from "src/app/shared/service/student.service";
import Swal from 'sweetalert2';

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
    title: "Visualizza Pagella", 
    img: "assets/dashboard/logoCircolari.jpg",
    quarter: [
      "1° Quadrimestre",
      "2° Quadrimestre"
    ]
  }

  examsTeachers!: TeacherExam[];
  examsStudents!: StudentExam[];
  order: string = 'Date';
  orderPdf: string = 'UploadDate'
  editForm!: FormGroup;
  selectReport!: FormGroup;
  pdfs!: PdfCirculars[];
  circularId!: string;
  currentDate = new Date();
  day = this.currentDate.getDate();
  month = this.currentDate.getMonth() + 1;
  year = this.currentDate.getFullYear();
  today = this.year + "-" + this.month + "-" + this.day;

  quadrimestreInizio1: number = 9;  // Settembre
  quadrimestreFine1: number = 1;    // Gennaio
  quadrimestreInizio2: number = 2;  // Febbraio
  quadrimestreFine2: number = 6;    // Giugno
  itemsPerPage = 3;


  constructor(
    private commonService: CommonService, 
    private classroomService: ClassroomService,
    private teacherService: TeacherService,
    private studentService: StudentService,
    private examsService: ExamsService,
    private fb: NonNullableFormBuilder,
    private route: ActivatedRoute,
    private userService: UsersService,

    private authService: AuthService ){ 

      this.editForm =this.fb.group({
      circularNumber: new FormControl (null, Validators.required),
      uploadDate: new FormControl(null, Validators.required,),
      location: new FormControl(null, Validators.required),
      object: new FormControl(null, Validators.required),
      header: new FormControl(null, Validators.required),
      body: new FormControl(null, Validators.required),
      sign: new FormControl(null, Validators.required),
      
    })

    this.selectReport = this.fb.group({

    })
  }

  ngOnInit(): void {
    this.usersMe()
    this.getCount()
    this.getExams(this.isTeacher);
    this.getCirculars();
  
    //this.getClassroomCount()
  }


  getCirculars() { 
    const params = new HttpParams()
    .set('Order', this.orderPdf)
    .set('OrderType', 'desc')
    this.commonService.getCirculars(params).subscribe({
      next: (res: ListResponse<PdfCirculars[]>) => {
        this.pdfs = res.data;
        console.log('get circolari', res.data);
      },
      error:(err) => {
        console.log("error",err);
      }
    })
  }

  addCircular() {
    const data=this.editForm.value
    this.commonService.addCirculars(data).subscribe((res) => {
      
  })
    this.getCirculars();
    this.showAlert();
    this.editForm.reset();
  }

   showAlert() {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'success',
      title: 'Creazione avvenuta con successo',
      showConfirmButton: false,
      timer: 2500,
      background: '#ffd45f'

    });
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
            .filter((item, index) => item.date >= this.today)
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
            .filter((item, index) => item.date >= this.today)
            .filter((_, index) => index < this.itemsPerPage);
        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    }
  }

  studentYears: string[] = []
  userData!: UsersMe;

  usersMe(){
    this.userService.getUsersMe().subscribe({
      next: (res: UsersMe) => {
        this.userData = res;
        console.log('get userMe',res)
      },
      error: (err) => {
        console.log('error', err);
      }
    })
    
  }

  getStudentYears(){
    this.studentService.getStudentsSchoolYears(this.userData.id).subscribe({
      next: (res) => {
        this.studentYears = res;
        console.log('years',this.studentYears);
      }

    })
  }

  isQuadrimestreInCorso(): boolean {
    const isCurrentQuadrimestre =
      (this.month >= this.quadrimestreInizio1 && this.month >= this.quadrimestreFine1);
  
    console.log('1° Quadrimestre in corso:', isCurrentQuadrimestre);
    console.log('mese corrente', this.month)

    return isCurrentQuadrimestre;
  
  }

  isQuadrimestreTerminato(): boolean {
    
    return !this.isQuadrimestreInCorso();
  }
  

}
  

  // getClassroomCount() {
  //   this.classroomService.getDataClassroom().subscribe(({total}) => {
  //     this.count.Classrooms = total;
  //     console.log("totale classi", total);
  //   });
  // }

