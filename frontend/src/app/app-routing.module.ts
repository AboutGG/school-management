import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './shared/helpers/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddUserComponent } from './components/pages/users/add-user/add-user.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, pathMatch: 'full'},
  { path: 'add-user', component: AddUserComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
