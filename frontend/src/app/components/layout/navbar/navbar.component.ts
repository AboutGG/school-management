import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { UsersMe } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { UsersService } from "src/app/shared/service/users.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Subject, takeUntil } from "rxjs";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent implements OnInit {
  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter<void>();

  constructor(
    private authService: AuthService,
    private usersService: UsersService
  ) {}

  users!: UsersMe;
  unsubscribe$: Subject<boolean> = new Subject<boolean>();
  selectedTab: string = "";
  accountForm!: FormGroup;

  // alert: boolean = false;
  error: boolean = false;
  successEdit: boolean = false;

  switch(prova: string): void {
    this.selectedTab = prova;
  }

  ngOnInit(): void {
    this.usersMe();
    this.accountForm = new FormGroup({
      oldpassword: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
      newpassword: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }

  handleSidebarToggle(): void {
    this.toggleSidebar.emit();
  }

  logout() {
    this.authService.logout();
  }

  usersMe() {
    this.usersService.getUsersMe().pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res: UsersMe) => {
        this.users = res;
        console.log("get me", res);
      },
      error: (err) => {
        console.log("error", err);
      },
    });
  }

  onSubmit() {
    this.authService
      .changePassword(this.accountForm.value, this.users.id)
      .subscribe({
        next: (res: any) => {
          this.successEdit = true;
          this.error = false;
        },
        error: (err) => {
          this.error = true;
          this.successEdit = false;
        },
      });
    this.accountForm.reset();
    console.log(this.accountForm.value);
  }

  showAlerts(formControlName: string): boolean {
    const formControl = this.accountForm.get(formControlName);
    return !!formControl && formControl.invalid;
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }
}
