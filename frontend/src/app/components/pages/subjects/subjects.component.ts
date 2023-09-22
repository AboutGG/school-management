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
  totalItems = 0;
  newPage! : string
  previousPage: number = 1;

  constructor(private classroomService: ClassroomService) {}


  ngOnInit(){
    this.fetchData();
    
  }

    // get dati api teacher subjects
    fetchData() {
      // const params = {
      //   page: this.currentPage,
      //   search: this.searchTerm,
      //   itemsPerPage: this.itemsPerPage,
      // }
      this.classroomService.getTeacherSubjects(new HttpParams).subscribe({
        next: (data: TeacherSubject []) => {
          this.totalItems = data.length; // numero totale di elementi

          const startIndex = (this.currentPage - 1) * this.itemsPerPage;
          const endIndex = startIndex + this.itemsPerPage;
          this.teachers = data.slice(startIndex, endIndex);

          console.log('dati get',data)
          // console.log('params',params)
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
