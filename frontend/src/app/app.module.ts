import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddUserComponent } from './components/pages/users/add-user/add-user.component';
import { JwtModule } from "@auth0/angular-jwt";
import { LoginComponent } from './components/pages/login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from '../app/shared/helpers/auth.interceptor';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { ClassesComponent } from './components/pages/classes/classes.component';
import { ShowClassComponent } from './components/pages/classes/show-class/show-class.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { ListUsersComponent } from './components/pages/users/list-users/list-users.component';
import { DetailUserComponent } from './components/pages/users/detail-user/detail-user.component';
import { NotFoundComponent } from './components/pages/not-found/not-found.component';
import { ExamslistComponent } from './components/pages/exams/examslist/examslist.component';
import { ExamsStudentListComponent } from './components/pages/exams/exams-student-list/exams-student-list.component';
import { SubjectsComponent } from './components/pages/subjects/subjects.component';
import { StudentSubjectsComponent } from './components/pages/student-subjects/student-subjects.component';
import { ExamDetailsComponent } from './components/pages/exams/exam-details/exam-details.component';

export function tokenGetter() {
  return localStorage.getItem("access_token");
}


@NgModule({
  declarations: [
    AppComponent,
    AddUserComponent,
    LoginComponent,
    NavbarComponent,
    SidebarComponent,
    ClassesComponent,
    ShowClassComponent,
    DashboardComponent,
    DashboardComponent,
    ListUsersComponent,
    DetailUserComponent,
    NotFoundComponent,
    ExamslistComponent,
    ExamsStudentListComponent,
    SubjectsComponent,
    StudentSubjectsComponent,
    ExamDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["example.com"],
        disallowedRoutes: ["http://example.com/examplebadroute/"],
      },
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
