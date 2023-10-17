import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Students, Teachers } from 'src/app/shared/models/users';
import { ClassDetails, TeacherClassroom } from 'src/app/shared/models/classrooms';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { HttpParams } from '@angular/common/http';
import { Form, FormControl, FormGroup } from '@angular/forms';
import { StudentGraduation } from 'src/app/shared/models/studentgraduation';

@Component({
  selector: "app-show-class",
  templateUrl: "./show-class.component.html",
  styleUrls: ["./show-class.component.scss"],
})
export class ShowClassComponent {
  classId!: string;
  classDetails!: ClassDetails;
  isTeacher: boolean = false; //memorizza se l'utente è insegnante
  order: string = "Registry.Surname";

  userGraduation!: StudentGraduation;
  // graduationForm!: FormGroup;

  constructor(
    private classroomService: ClassroomService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.classId = params["id"];
    });
    this.fetchClassDetails();
    this.isTeacher = this.authService.isTeacher();

    // this.graduationForm = new FormGroup({
    //   schoolYear: new FormControl(null),
    // });
  }

  fetchClassDetails() {
    const params = new HttpParams().set("Order", this.order);
    this.classroomService.getSingleClassroom(this.classId, params).subscribe({
      next: (res: ListResponse<ClassDetails>) => {
        this.classDetails = res.data;
        console.log(res.data);
      },
      error: (err) => {
        console.log("error", err);
      },
    });
  }

  navigateToTeachersClasses() {
    this.router.navigate(["teachers/classes"]);
  }
}
