import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Teachers } from 'src/app/shared/models/users';
import { ClassroomService } from 'src/app/shared/service/classroom.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss']
})
export class SubjectsComponent {

  teacherSubjects : any;

  constructor(private classroomService: ClassroomService) {}


  ngOnInit(){
    
  }

    // get dati api classroom
    fetchData() {
      this.classroomService.getTeacherSubjects().subscribe({
        next: (data: any []) => {
          this.teacherSubjects = data;
          console.log(data);
        },
        error: (err) => { 
          console.log("errore",err);
      }
    })
    }





}
