import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { SubjectService } from "src/app/shared/service/subject.service";
import { StudentSubject } from "src/app/shared/models/subjects";
import { ListResponse } from "src/app/shared/models/listresponse";

@Component({
  selector: "app-subjects",
  templateUrl: "./student-subjects.component.html",
  styleUrls: ["./student-subjects.component.scss"],
})
export class StudentSubjectsComponent implements OnInit {
  students: StudentSubject[] = [];
  searchQuery: string = "";
  currentPage: number = 1;
  itemsPerPage: number = 5; // numero di elementi per pagina
  totalItems!: number;
  newPage!: string;
  previousPage: number = 1;
  totalPages!: number;
  this: any;

  constructor(private subjectService: SubjectService) {
    
  }

  ngOnInit() {
    this.fetchDataStudents();  
  }

  fetchDataStudents() {
    console.log(this.searchQuery);
    
    const params = new HttpParams()
      .set("Page", this.currentPage)
      .set("Search", this.searchQuery)
      .set("ItemsPerPage", this.itemsPerPage);
    this.subjectService.getSubjects(params).subscribe({
      next: (res: ListResponse<StudentSubject[]>) => {
        this.students = res.data;
        this.totalItems = res.total;
        this.totalPages = this.totalItems / this.itemsPerPage;
      },
      error: (err) => {
        console.log("error", err);
      },
    });
  }

  searchSubjects() {
    this.previousPage = this.currentPage;
    this.currentPage = 1;
    this.fetchDataStudents();
  }
  
  //funzione per paginazione
  onPageChange(newPage: number) {
    this.currentPage = newPage;
    this.fetchDataStudents();
    console.log("page", this.currentPage);
  }
}
