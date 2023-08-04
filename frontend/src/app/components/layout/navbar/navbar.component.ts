import { Component, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent {
  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter<void>();

  handleSidebarToggle(): void {
    this.toggleSidebar.emit();
  }
}
