import { Component } from "@angular/core";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent {
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
}
