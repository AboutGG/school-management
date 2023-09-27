import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Students, Teachers } from 'src/app/shared/models/users';
import { ClassDetails, Classroom } from 'src/app/shared/models/classrooms';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { ListResponse } from 'src/app/shared/models/listResponse';

@Component({
  selector: 'app-show-class',
  templateUrl: './show-class.component.html',
  styleUrls: ['./show-class.component.scss']
})
export class ShowClassComponent {
  class: Classroom []=[]
  classId!: string;
  classDetails!: ClassDetails;
  isTeacher!: boolean; //memorizza se l'utente è insegnante
  

  constructor(private classroomService: ClassroomService, private authService: AuthService, private route: ActivatedRoute) {}
  

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.classId = params['id'];
    });
    this.fetchClassDetails();
    this.isTeacher = this.authService.isTeacher()

    
  }

  fetchClassDetails() {
    this.classroomService.getSingleClassroom(this.classId).subscribe({
      next: (res: ListResponse<ClassDetails>) => {
        this.classDetails = res.data;
        console.log(res.data);
      },
      error:(err) => {
        console.log("error",err);
      }

    })
  }

  


}
