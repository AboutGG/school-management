import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
    this.fetchData();
  }

    // get dati api classroom
    fetchData() {
      this.classroomService.getDataClassroom().subscribe({
        next: (data: any) => {
          this.teacherSubjects = data;
          console.log(data);
        },
        error: (err) => { 
          console.log("errore",err);
      }
    })
    }





}
