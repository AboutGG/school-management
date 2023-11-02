import { Subject, takeUntil } from 'rxjs';
import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { UsersMe } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { UsersService } from "src/app/shared/service/users.service";

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"]
})
export class SidebarComponent implements OnInit, OnDestroy{
  constructor(private authService: AuthService, private usersService: UsersService) { }

  @Input() isExpanded: boolean = false;
  isCollapsed = false;
  isTeacher = this.authService.isTeacher()
  linkByRole!: string
  userMe!: UsersMe
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  ngOnInit(): void {
    this.usersMe();
    this.routerSwitchByRole()
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }

  routerSwitchByRole () {
    this.linkByRole = this.isTeacher ? 'teachers' : 'students';
  }
  
  usersMe(){
    this.usersService.getUsersMe().pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res: UsersMe) => {
        this.usersService.userMe.next(res);
        this.userMe = this.usersService.getUserMeValue();
        console.log("userMe: ", this.userMe);
      },
      error: (err) => {
        console.log('error', err);
      }
    })
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }

}
