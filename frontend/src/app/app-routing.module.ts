import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./shared/helpers/auth.guard";
import { RoleGuard } from "./shared/helpers/role.guard";
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { AddUserComponent } from "./components/pages/users/add-user/add-user.component";
import { NotFoundComponent } from "./components/pages/not-found/not-found.component";
import { ClassesComponent } from './components/pages/classes/classes.component';
import { ShowClassComponent } from './components/pages/classes/show-class/show-class.component';
import { SubjectsComponent } from "./components/pages/subjects/subjects.component";
import { ListUsersComponent } from './components/pages/users/list-users/list-users.component';
import { DetailUserComponent } from './components/pages/users/detail-user/detail-user.component';
import { ExamsStudentListComponent } from "./components/pages/exams-student-list/exams-student-list.component";
import { ExamslistComponent } from "./components/pages/examslist/examslist.component";
import { StudentSubjectsComponent } from "./components/pages/student-subjects/student-subjects.component";
import { ExamDetailsComponent } from "./components/pages/exam-details/exam-details.component";

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: "dashboard", component: DashboardComponent, canActivate: [AuthGuard], pathMatch: 'full'},
  { path: "login", component: LoginComponent },
  { path: "add-user", component: AddUserComponent, canActivate: [AuthGuard, RoleGuard] },
  { path: 'classes/:id', component: ShowClassComponent, canActivate: [AuthGuard] },
  { path: "teachers/subjects", component:SubjectsComponent, canActivate: [AuthGuard] },
  { path: "students/subjects", component:StudentSubjectsComponent, canActivate: [AuthGuard] },
  { path: "add-user/:id", component: AddUserComponent, canActivate: [AuthGuard, RoleGuard] },
  { path: 'teachers/classes', component: ClassesComponent, canActivate: [AuthGuard] },
  { path: 'students/classes/:id', component: ShowClassComponent, canActivate: [AuthGuard] },
  { path: 'classes/:id', component: ShowClassComponent, canActivate: [AuthGuard]},
  { path: "not-found/:statusCode", component: NotFoundComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: 'list-users', component: ListUsersComponent, canActivate: [AuthGuard, RoleGuard]},
  { path: 'details/:id', component: DetailUserComponent, canActivate: [AuthGuard] },
  { path: 'students/exams', component: ExamsStudentListComponent, canActivate: [AuthGuard] },
  { path: 'teachers/exams', component: ExamslistComponent, canActivate: [AuthGuard] },
  { path: 'teachers/exams/:id', component: ExamDetailsComponent, canActivate: [AuthGuard] },
  { path: "**", redirectTo: "dashboard" },
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
