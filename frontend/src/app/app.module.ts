import { NgModule } from '@angular/core';
import { NgModel, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddUserComponent } from './components/pages/users/add-user/add-user.component';

import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from '../app/shared/helpers/auth.interceptor';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { ClassesComponent } from './components/pages/classes/classes.component';
import { ShowClassComponent } from './components/pages/classes/show-class/show-class.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { ListUsersComponent } from './components/pages/users/list-users/list-users.component';
import { DetailUserComponent } from './components/pages/users/detail-user/detail-user.component';
import { EditUserComponent } from './components/pages/users/edit-user/edit-user.component';
import { NotFoundComponent } from './components/pages/not-found/not-found.component';
import { ErrorsInterceptor } from './shared/helpers/errors.interceptor';
import { SubjectsComponent } from './components/pages/subjects/subjects.component';

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
    EditUserComponent,
    NotFoundComponent,
    SubjectsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorsInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
