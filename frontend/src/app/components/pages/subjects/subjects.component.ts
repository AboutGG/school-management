import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ListResponse, StudentSubjects } from 'src/app/shared/models/users';
import { SubjectService } from 'src/app/shared/service/subject.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss'],
})
export class SubjectsComponent implements OnInit {
  teachers: StudentSubjects[] = []; 

  searchQuery: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 1;
  previousPage: number = 1;
  totalItems!: number ;
  totalPages!: number ;
  constructor(private subjectService: SubjectService) {}

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    const params = new HttpParams()
        .set('Page', this.currentPage)
        .set('Search', this.searchQuery)
        .set('ItemsPerPage', this.itemsPerPage);


    this.subjectService.getSubjects(params).subscribe({
      next: (res: ListResponse) => {
        this.teachers = res.data;
        this.totalItems = res.total;
        this.totalPages = this.totalItems/this.itemsPerPage;

      },
      error: (err) => {
        console.log('errore', err);
      },
    });
  }

  searchSubjects() {
    this.previousPage = this.currentPage;
    this.currentPage = 1;
    this.fetchData();
  
  }

  onPageChange(newPage: number) {
    this.currentPage = newPage;
    this.fetchData();
    console.log('page',this.currentPage);
    }
}
