import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Classroom } from 'src/app/shared/models/users';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.scss']
})
export class ClassesComponent {

  class: Classroom[] = [];
  searchTerm!: string;
  isTeacher!: boolean;

  constructor(private classroomService: ClassroomService, private authService: AuthService) {}

  ngOnInit(){
    this.fetchData();
    this.isTeacher = this.authService.isTeacher()
  }

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

    //funzione per ricerca
    onSearch() {
      this.classroomService.searchClassrooms(this.searchTerm).subscribe((data) => {
        this.class = data;
      });
    }
    

}
