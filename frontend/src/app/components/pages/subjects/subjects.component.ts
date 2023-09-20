import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentSubjects, Teachers } from 'src/app/shared/models/users';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { SubjectService } from 'src/app/shared/service/subject.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss']
})
export class SubjectsComponent {

  teacherSubjects : StudentSubjects [] = [];

  constructor(private subjectService: SubjectService) {}


  ngOnInit(){
    this.fetchData();
    
  }

    // get dati api classroom
    fetchData() {
      this.subjectService.getSubjects().subscribe({
        next: (data: StudentSubjects [] ) => {
          this.teacherSubjects = data;
          console.log(data);
        },
        error: (err) => { 
          console.log("errore",err);
      }
    })
    }





}
