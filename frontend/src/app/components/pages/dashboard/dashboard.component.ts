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
  pdfs: string[] = [
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg",
    "assets/dashboard/logoCircolari.jpg"
  ];

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
