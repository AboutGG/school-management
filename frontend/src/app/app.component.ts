import { Component } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'school-management';

  sidebarExpanded: boolean = true;

  constructor(public router: Router) {}

  handleSidebarToggle(): void {
    this.sidebarExpanded = !this.sidebarExpanded;
  }
}
