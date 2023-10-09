import {Component, EventEmitter, inject, OnInit, Output} from "@angular/core";
import { UsersMe } from "src/app/shared/models/users";
import { AuthService } from "src/app/shared/service/auth.service";
import { UsersService } from "src/app/shared/service/users.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../../../shared/service/account.service";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent implements OnInit {
  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter<void>();

  accountService = inject(AccountService);

  constructor(private authService: AuthService, private usersService: UsersService) { }

  users! : UsersMe;
  selectedTab: string = "";
  accountForm!: FormGroup;

  switch(prova: string):void{
    this.selectedTab = prova;
  }

  ngOnInit(): void {
    this.usersMe();
    this.accountForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      oldpassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      newpassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
    });
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

  onSubmit()
  {
    this.accountService.putUser(this.accountForm.value, this.users.id).subscribe()
    console.log(this.accountForm.value)
  }
}
