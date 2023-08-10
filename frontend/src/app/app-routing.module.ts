import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './shared/helpers/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddUserComponent } from './components/pages/users/add-user/add-user.component';
import { ClassesComponent } from './components/pages/classes/classes.component';
import { ShowClassComponent } from './components/pages/classes/show-class/show-class.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, pathMatch: 'full'},
  { path: 'add-user', component: AddUserComponent, canActivate: [AuthGuard] },
  { path: 'classes', component: ClassesComponent, canActivate: [AuthGuard] },
  { path: 'classes/:id', component: ShowClassComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
