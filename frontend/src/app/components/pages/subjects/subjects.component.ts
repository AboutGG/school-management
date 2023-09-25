import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TeacherSubject, Teachers } from 'src/app/shared/models/users';
import { ClassroomService } from 'src/app/shared/service/classroom.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss']
})
export class SubjectsComponent {

  teachers: TeacherSubject [] = [];
  searchTerm: string = '';
  currentPage : number = 1; 
  itemsPerPage : number = 1// numero di elementi per pagina
  totalItems! : number;
  newPage! : string
  previousPage: number = 1;
  totalPages!: number;

  constructor(private classroomService: ClassroomService) {}


  ngOnInit(){
    this.fetchData();
    
  }

    // get dati api teacher subjects
    fetchData() {
       const params = new HttpParams()
        .set('Page', this.currentPage)
        .set('Search', this.searchTerm)
        .set('ItemsPerPage', this.itemsPerPage);
      this.classroomService.getTeacherSubjects(params).subscribe({
        next: (res: any) => {
          this.totalItems = res.total; // numero totale di elementi
          this.totalPages = this.totalItems/this.itemsPerPage;
          this.teachers = res.data;

          console.log('dati get',res.data)
          console.log('params',params)
        },
        error: (err) => { 
          console.log("error",err);
        }
      })
    }

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
