import { Component, EventEmitter, Output } from "@angular/core";
import { AuthService } from "src/app/shared/service/auth.service";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent {
  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter<void>();

  /**
   *
   */
  constructor(private authService: AuthService) { }

  handleSidebarToggle(): void {
    this.toggleSidebar.emit();
  }

  logout() {
    this.authService.logout();
  }
}
