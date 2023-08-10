import { Component, OnInit } from "@angular/core";
import { TypeCount } from "src/app/shared/models/users";
import { CommonService } from "src/app/shared/service/common.service";
import { UsersService } from "src/app/shared/service/users.service";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {

  count!: TypeCount

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

  constructor(private commonService: CommonService) { }

  ngOnInit(): void {
    this.getCount()
  }

  getCount() {
    this.commonService.getCount().subscribe((res) => {
      this.count = res
    })
  }




}
