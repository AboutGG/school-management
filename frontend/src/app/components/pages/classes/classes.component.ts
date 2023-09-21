import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Classroom } from 'src/app/shared/models/users';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.scss']
})
export class ClassesComponent {

  class: Classroom[] = [];
  searchTerm: string = '';
  currentPage : number = 1; 
  itemsPerPage : number = 1// numero di elementi per pagina
  totalItems = 0;
  isTeacher!: boolean;
  newPage! : string


  constructor(private classroomService: ClassroomService, private authService: AuthService, private route: ActivatedRoute) {}

  ngOnInit(){
 
    this.fetchData();
  }

    // get dati api classroom
    fetchData() {
      this.classroomService.getDataClassroom({search: this.searchTerm, page: this.currentPage, itemPerPage: this.itemsPerPage} ).subscribe({
        next: (data: Classroom []) => {
          this.class = data;
          this.class = this.class.slice(0, this.itemsPerPage); // mostra solo i primi 10 elementi inizialmente
          this.totalItems = this.class.length;
         
          console.log('dati get',data);
          console.log('data search',this.searchTerm)   
        },
        error: (err) => { 
          console.log("error",err);
      }
    })
    }

    //funzione per ricerca
    onSearch() { 
      this.fetchData();
      
    }

    //funzione per paginazione
    onPageChange(currentPage: number) {
      
        this.fetchData();
        console.log('page',this.currentPage);
      }
}
