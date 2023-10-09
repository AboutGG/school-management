import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { UsersMe } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { UsersService } from "src/app/shared/service/users.service";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent implements OnInit {
  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter<void>();

  /**
   *
   */
  constructor(private authService: AuthService, private usersService: UsersService) { }

  users! : UsersMe;

  ngOnInit(): void {
    this.usersMe();
  }

  handleSidebarToggle(): void {
    this.toggleSidebar.emit();
  }

  logout() {
    this.authService.logout();
  }

  usersMe(){
    this.usersService.getUsersMe().subscribe({
      next: (res: UsersMe) => {
        this.users = res;
        console.log('get me',res)

      },
      error: (err) => {
        console.log('error', err);
      }
    })
    
  }
}
