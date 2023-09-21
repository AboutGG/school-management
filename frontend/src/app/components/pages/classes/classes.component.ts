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
  previousPage: number = 1;


  constructor(private classroomService: ClassroomService, private authService: AuthService, private route: ActivatedRoute) {}

  ngOnInit(){
 
    this.fetchData();
  }

    // get dati api classroom
    fetchData() {
      const params = {
        search: this.searchTerm,
        page: this.currentPage,
        itemsPerPage: this.itemsPerPage,
      };
      this.classroomService.getDataClassroom(params).subscribe({
        next: (data: Classroom[]) => {
          this.totalItems = data.length; // numero totale di elementi
  
          const startIndex = (this.currentPage - 1) * this.itemsPerPage;
          const endIndex = startIndex + this.itemsPerPage;
          
          this.class = data.slice(startIndex, endIndex);
  
          console.log('dati get', data);
          console.log('params', params)
        },
        error: (err) => {
          console.log('error', err);
        }
      });
    }
  

    //funzione per ricerca
    onSearch() { 
      this.previousPage = this.currentPage;
      this.currentPage = 1;
     
      this.fetchData();
      
    }

    //funzione per paginazione
    onPageChange(newPage: number) {
      this.currentPage = newPage;
        this.fetchData();
        console.log('page',this.currentPage);
      }
}
