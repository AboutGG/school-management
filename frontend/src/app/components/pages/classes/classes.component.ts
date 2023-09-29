import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { HttpParams } from '@angular/common/http';
import { Classroom } from 'src/app/shared/models/classrooms';
import { ListResponse } from 'src/app/shared/models/listResponse';

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.scss']
})
export class ClassesComponent {

  class: Classroom[] = [];
  searchTerm: string = '';
  currentPage : number = 1; 
  itemsPerPage : number = 5// numero di elementi per pagina
  totalItems! : number;
  isTeacher!: boolean;
  previousPage: number = 1;
  totalPages!: number;
  order: string = 'name';


  constructor(private classroomService: ClassroomService) {}

  ngOnInit(){
 
    this.fetchData();
  }

    // get dati api classroom
    fetchData() {
         const params = new HttpParams()
        .set('Page', this.currentPage)
        .set('Search', this.searchTerm)
        .set('Order', this.order)
        .set('ItemsPerPage', this.itemsPerPage);
        
        
      this.classroomService.getDataClassroom(params).subscribe({
        next: (res: ListResponse<Classroom[]> ) => {
          this.totalItems = res.total; // numero totale di elementi
          this.totalPages = this.totalItems/this.itemsPerPage;
          this.class = res.data;
  
          console.log('dati get', res.data);
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
