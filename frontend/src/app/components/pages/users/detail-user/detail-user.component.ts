import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Registry } from "src/app/shared/models/users";
import { UsersService } from "src/app/shared/service/users.service";
import { AuthService } from "src/app/shared/service/auth.service";
import { Subject, takeUntil } from "rxjs";


@Component({
  selector: "app-detail-user",
  templateUrl: "./detail-user.component.html",
  styleUrls: ["./detail-user.component.scss"],
})
export class DetailUserComponent {
  id!: string;
  details!: Registry;
  isTeacher = this.authService.isTeacher();
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  constructor(
    private usersService: UsersService,
    private route: ActivatedRoute,
    private authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params["id"];
    console.log(this.id);
    this.usersService.getDetailsUser(this.id).pipe(takeUntil(this.unsubscribe$)).subscribe((res: Registry) => {
      console.log(res);
      this.details = res;
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete(); 
  }
}
