import { Component, OnInit } from "@angular/core";
import { TypeCount } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { ClassroomService } from "src/app/shared/service/classroom.service";
import { CommonService } from "src/app/shared/service/common.service";
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
  isTeacher = this.authService.isTeacher()

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

  constructor(
    private commonService: CommonService, 
    private classroomService: ClassroomService,
    private teacherService: TeacherService,
    private authService: AuthService ){ }

  ngOnInit(): void {
    //this.getCount()
    this.getClassroomCount()
  }

  getCount() {
    this.commonService.getCount().subscribe((res) => {
      this.count = res
    })
  }

  getClassroomCount()
  {
    this.classroomService.getDataClassroom().subscribe(({total}) => {
      this.count.Classrooms = total;
      console.log("totale", total);
    });
  }

  getSubjectCount()
  {
    
  }
}
