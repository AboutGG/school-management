import { Component } from "@angular/core";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent {
  pdfs: string[] = [
    "/school-management/frontend/src/assets/dashboard/logoCircolari.jpg",
    "/school-management/frontend/src/assets/dashboard/logoCircolari.jpg",
    "/school-management/frontend/src/assets/dashboard/logoCircolari.jpg",
  ];
}
