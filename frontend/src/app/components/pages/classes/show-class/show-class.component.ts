import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClassDetails, Students, Teachers } from 'src/app/shared/models/users';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';

@Component({
  selector: 'app-show-class',
  templateUrl: './show-class.component.html',
  styleUrls: ['./show-class.component.scss']
})
export class ShowClassComponent {

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
      next: (data: ClassDetails) => {
        this.classDetails = data;
        console.log(data);
      },
      error:(err) => {
        console.log("errore",err);
      }

    })
  }


}
