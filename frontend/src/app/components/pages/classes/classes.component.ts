import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Classroom } from 'src/app/shared/models/users';
import { ClassroomService } from 'src/app/shared/service/classroom.service';

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.scss']
})
export class ClassesComponent {

  class: Classroom[] = [];

  constructor(private classroomService: ClassroomService) {}


    // get dati api classroom
    fetchData() {
      this.classroomService.getDataClassroom().subscribe({
        next: (data: Classroom []) => {
          this.class = data;
          console.log(data);
        },
        error: (err) => { 
          console.log("errore",err);
      }
    })
    }

}
