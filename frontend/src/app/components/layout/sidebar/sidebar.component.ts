import { Component, Input } from "@angular/core";
import { AuthService } from "src/app/shared/service/auth.service";

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"]
})
export class SidebarComponent {
  constructor (private authService: AuthService) {}
  @Input() isExpanded: boolean = false;
  isCollapsed = false;
  linkByRole! :string;
  isTeacher = this.authService.isTeacher();

  ngOnInit (){
    this.routerSwitchByRole();
  }


  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }

  routerSwitchByRole () {
   this.linkByRole = this.isTeacher ? 'teachers' : 'students';
  }
}
