import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./shared/helpers/auth.guard";
import { DashboardComponent } from "./components/dashboard/dashboard.component";

// const routes: Routes = [];
const routes: Routes = [
  { path: "", redirectTo: "dashboard", pathMatch: "full" },
  { path: "login", component: LoginComponent, pathMatch: "full" },
  { path: "dashboard", component: DashboardComponent, pathMatch: "full", canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
