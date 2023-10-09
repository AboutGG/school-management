import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { TeacherExam } from "src/app/shared/models/teacherexam";
import { ListResponse, TypeCount } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { ClassroomService } from "src/app/shared/service/classroom.service";
import { CommonService } from "src/app/shared/service/common.service";
import { ExamsService } from "src/app/shared/service/exams.service";
import { TeacherService } from "src/app/shared/service/teacher.service";
import { UsersService } from "src/app/shared/service/users.service";

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

  pagella = {title: "Pagella 1° Quadrimestre", img: "assets/dashboard/logoCircolari.jpg"}

  pdfs = [
    {
      title: "Nuovo ordinamento scolastico",
      data: "10/10/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
    {
      title: "Fine anno scolastico",
      data: "01/06/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
    {
      title: "Programma esami di stato",
      data: "16/05/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
    {
      title: "Convocazione consiglio d'istituto",
      data: "28/04/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
    {
      title: "Assemblea d'istituto",
      data: "10/04/2023",
      img: "assets/dashboard/logoCircolari.jpg"

    },
    {
      title: "Programma Carnevale a scuola",
      data: "03/02/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
    {
      title: "Incontro scuola famiglia",
      data: "12/01/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
    {
      title: "Circolare di fine anno solare",
      data: "21/12/2023",
      img: "assets/dashboard/logoCircolari.jpg"
    },
  ]
  exams!: TeacherExam[];
  itemsPerPage: number = 3;


  constructor(
    private commonService: CommonService, 
    private classroomService: ClassroomService,
    private teacherService: TeacherService,
    private examsService: ExamsService,
    private authService: AuthService ){ }

  ngOnInit(): void {
    this.getCount()
    this.getExams(this.isTeacher);
    //this.getClassroomCount()
  }

  getCount() {
    this.commonService.getCount().subscribe((res) => {
      this.count = res;
    })
  }

  getExams(isTeacher: boolean) {
    const params = new HttpParams()
    .set('ItemsPerPage', this.itemsPerPage)

    if (isTeacher) {
      this.examsService.getTeacherExams(params).subscribe({
        next: (res: ListResponse<TeacherExam[]>) => {
          this.exams = res.data;
        },
        error: (err) => {
          console.log('error dash', err);
        }
      });
    } else {
      this.examsService.getStudentExams().subscribe({
        next: (res: ListResponse<TeacherExam[]>) => {
          this.exams = res.data;
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
