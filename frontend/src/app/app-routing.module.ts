import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./shared/helpers/auth.guard";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { AddUserComponent } from "./components/pages/users/add-user/add-user.component";
import { NotFoundComponent } from "./components/pages/not-found/not-found.component";

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: "dashboard", component: DashboardComponent, canActivate: [AuthGuard]},
  { path: "login", component: LoginComponent },
  { path: "add-user", component: AddUserComponent, canActivate: [AuthGuard] },
  { path: "not-found/:statusCode", component: NotFoundComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: "**", redirectTo: "not-found" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
