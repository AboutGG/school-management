import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { PdfCirculars } from "src/app/shared/models/pdf";
import { StudentExam, TeacherExam } from "src/app/shared/models/exams";
import { UsersMe } from "src/app/shared/models/users";
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
import { Subject, takeUntil } from "rxjs";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {

  count: any;

  isTeacher = this.authService.isTeacher();
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  examsTeachers!: TeacherExam[];
  examsStudents!: StudentExam[];
  order: string = 'Date';
  orderPdf: string = 'UploadDate';
  editForm!: FormGroup;
  pdfs!: PdfCirculars[];
  circularId!: string;
  currentDate = new Date();
  day = this.currentDate.getDate();
  month = this.currentDate.getMonth() + 1;
  year = this.currentDate.getFullYear();
  today = this.year + "-" + this.month + "-" + this.day;
  todayExams = new Date(new Date().getTime()).toISOString().substring(0,10);

  quadrimestreInizio1: number = 9;  // Settembre
  quadrimestreFine1: number = 1;    // Gennaio
  quadrimestreInizio2: number = 2;  // Febbraio
  quadrimestreFine2: number = 6;    // Giugno
  itemsPerPage = 3; // per prossimi esami 

  studentYears: string[] = [];
  userData!: UsersMe;
  isCurrentQuadrimestre!: boolean;
  schoolYear!: string;
  chosenQuadrimestre: boolean = true;


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
      uploadDate: new FormControl(this.todayExams, [Validators.required],),
      location: new FormControl(null, Validators.required),
      object: new FormControl(null, Validators.required),
      header: new FormControl(null, Validators.required),
      body: new FormControl(null, Validators.required),
      sign: new FormControl(null, Validators.required),
      
    })
  }

  ngOnInit(): void {
    this.usersMe()
    this.getCount()
    this.getExams(this.isTeacher);
    this.getCirculars();
    this.isQuadrimestreInCorso();
    //this.getClassroomCount()
  }


  // get circolari 
  getCirculars() { 
    const params = new HttpParams()
    .set('Order', this.orderPdf)
    .set('OrderType', 'desc')
    this.commonService.getCirculars(params).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res: ListResponse<PdfCirculars[]>) => {
        this.pdfs = res.data;
        console.log('get circolari', res.data);
      },
      error:(err) => {
        console.log("error",err);
      }
    })
  }

  // aggiunta circolare con form group 
  addCircular() {
    const data=this.editForm.value;
     
    this.editForm.reset();

    // ripristina la data odierna
    this.editForm.get('uploadDate')?.setValue(this.todayExams);

    this.commonService.addCirculars(data).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (data) => {
        console.log(data)
      }
    })
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'success',
      title: 'Creazione avvenuta con successo',
      showConfirmButton: false,
      timer: 2500,
    });
    
    this.getCirculars();
  
   
  }


  // pdf circolari 
  getCircularsById(pdf: PdfCirculars){
    this.commonService.getCircularsById(pdf.id).pipe(takeUntil(this.unsubscribe$)).subscribe( blob => {
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


  // count users, classi, studenti, insegnanti 
  getCount() {
    this.commonService.getCount().pipe(takeUntil(this.unsubscribe$)).subscribe((res) => {
      this.count = res;
    })
  }

  //get prossimi esami insegnante e studente
  getExams(isTeacher: boolean) {
    const params = new HttpParams()
    .set('Order', this.order)
    
    if (isTeacher) {
      this.examsService.getTeacherExams(params).subscribe({
        next: (res: ListResponse<TeacherExam[]>) => {

          this.examsTeachers = res.data
            .filter((item, index) => item.date >= this.todayExams)
            .filter((_, index) => index < this.itemsPerPage); 
            console.log(this.examsTeachers);
            console.log();
            
            
        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    } else {
      this.examsService.getStudentExams(params).pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: (res: ListResponse<StudentExam[]>) => {
          
          this.examsStudents = res.data
            .filter((item, index) => item.date >= this.todayExams)
            .filter((_, index) => index < this.itemsPerPage)    
            
        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    }
  }

  // me per prendere l'id user
  usersMe(){
    this.userService.getUsersMe().pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res: UsersMe) => {
        this.userData = res;
        this.getStudentYears();
      },
      error: (err) => {
        console.log('error', err);
      }
    }) 
  }

  // get anni scolastici studente
  getStudentYears(){
    this.studentService.getStudentsSchoolYears(this.userData.id).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res) => {
        this.studentYears = res;
        this.schoolYear = this.studentYears[0];
        console.log('years',this.studentYears);
      }
    })
  }

  // scelta quadrimestre
  chooseQuadrimestre(chosenQuadrimestre: boolean){
    if (chosenQuadrimestre){
      this.chosenQuadrimestre = true;

    } else {
      this.chosenQuadrimestre = false;
    }
    console.log('chosenQuadrimestre',this.chosenQuadrimestre);

  }

  // pdf pagelle
  getStudentsReports(){
    const params = new HttpParams()
    .set('firstQuarter', this.chosenQuadrimestre)
    .set('schoolYear', this.schoolYear)
    console.log(this.chosenQuadrimestre);
    console.log(this.schoolYear);
    
    this.studentService.getStudentsReports(this.userData.id, params).pipe(takeUntil(this.unsubscribe$)).subscribe( blob => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'pagella.pdf';
      a.style.display = 'none';
      document.body.appendChild(a); 
      a.click(); 
      window.URL.revokeObjectURL(url);
    }, (error) => {
      
      Swal.fire({
        toast: true,
        position: 'top-end',
        icon: 'warning',
        title: 'Pagella non ancora disponibile',
        showConfirmButton: false,
        timer: 2500,
  
      });

    })
  }

  // funzione per controllo mese corrente
  isQuadrimestreInCorso(): boolean {
     this.isCurrentQuadrimestre =
      (this.month >= this.quadrimestreInizio1 && this.month >= this.quadrimestreFine1);
  
    console.log('1° Quadrimestre in corso:', this.isCurrentQuadrimestre);
    console.log('mese corrente', this.month)

    return this.isCurrentQuadrimestre;
  
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete(); 
  }
}
