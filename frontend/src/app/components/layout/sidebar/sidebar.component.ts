import { Component, Input, OnInit } from "@angular/core";
import { AuthService } from "src/app/shared/service/auth.service";

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"]
})
export class SidebarComponent implements OnInit{
  /**
   *
   */
  constructor(private authService: AuthService) { }

  @Input() isExpanded: boolean = false;
  isCollapsed = false;
  isTeacher = this.authService.isTeacher()
  linkByRole!: string

  ngOnInit(): void {
    this.routerSwitchByRole()
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }

  routerSwitchByRole () {
    this.linkByRole = this.isTeacher ? 'teacher/exams' : 'student/exams';
  }
}
