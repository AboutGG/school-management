import { Component, Input, OnInit } from "@angular/core";
import { UsersMe } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { UsersService } from "src/app/shared/service/users.service";

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"]
})
export class SidebarComponent implements OnInit{
  constructor(private authService: AuthService, private usersService: UsersService) { }

  @Input() isExpanded: boolean = false;
  isCollapsed = false;
  isTeacher = this.authService.isTeacher()
  linkByRole!: string
  userId!: UsersMe;

  ngOnInit(): void {
    this.routerSwitchByRole()
    this.usersMe();
    
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }

  routerSwitchByRole () {
    this.linkByRole = this.isTeacher ? 'teachers' : 'students';
  }
  
  usersMe(){
    this.usersService.getUsersMe().subscribe({
      next: (res: UsersMe) => {
        this.userId = res;
        console.log('get me',res)

      },
      error: (err) => {
        console.log('error', err);
      }
    })
    
  }
}
