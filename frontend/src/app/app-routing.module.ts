import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './shared/helpers/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddUserComponent } from './components/pages/users/add-user/add-user.component';
import { ListUsersComponent } from './components/pages/users/list-users/list-users.component';
import { DetailUserComponent } from './components/pages/users/detail-user/detail-user.component';
import { EditUserComponent } from './components/pages/users/edit-user/edit-user.component';

// const routes: Routes = [];
const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent, pathMatch: 'full'},
  { path: 'add-user', component: AddUserComponent, canActivate: [AuthGuard] },
  { path: 'list-users', component: ListUsersComponent, canActivate: [AuthGuard] },
  { path: 'edit-user', component: EditUserComponent, canActivate: [AuthGuard] },
  { path: 'details', component: DetailUserComponent, canActivate: [AuthGuard] },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
