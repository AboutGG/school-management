import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Teacher, Teachers } from 'src/app/shared/models/users';
import { ClassroomService } from 'src/app/shared/service/classroom.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss']
})
export class SubjectsComponent implements OnInit {

  teachers!: Teacher

  constructor(private classroomService: ClassroomService) {}


  ngOnInit(){
    this.fetchData();
    
  }

    // get dati api teacher subjects
    fetchData() {
      this.classroomService.getTeacherSubjects().subscribe({
        next: (data: Teacher) => {
          this.teachers = data;
          console.log(data)
        },
        error: (err) => { 
          console.log("error",err);
        }
      })
    }





}