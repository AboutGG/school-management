import { Component } from '@angular/core';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { HttpParams } from '@angular/common/http';
import { TeacherClassroom } from 'src/app/shared/models/classrooms';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: "app-classes",
  templateUrl: "./classes.component.html",
  styleUrls: ["./classes.component.scss"],
})
export class ClassesComponent {
  class: TeacherClassroom[] = [];
  searchTerm: string = "";
  currentPage: number = 1;
  itemsPerPage: number = 5; // numero di elementi per pagina
  totalItems!: number;
  isTeacher!: boolean;
  previousPage: number = 1;
  totalPages!: number;
  order: string = "name";

  visiblePages: number[] = [];

  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  constructor(private classroomService: ClassroomService) {}

  ngOnInit() {
    this.fetchData();
    this.updateVisiblePages();
  }

  // get dati api classroom
  fetchData() {
    const params = new HttpParams()
      .set("Page", this.currentPage)
      .set("Search", this.searchTerm)
      .set("Order", this.order)
      .set("ItemsPerPage", this.itemsPerPage);

    this.classroomService.getDataClassroom(params).pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: (res: ListResponse<TeacherClassroom[]>) => {
          this.totalItems = res.total; // numero totale di elementi
          this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
          this.class = res.data;
          this.updateVisiblePages();

          console.log('total pages',this.totalPages);
          
        },
        error: (err) => {
          console.log("error", err);
        },
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
    this.updateVisiblePages();
    this.fetchData();
    console.log("current page", this.currentPage);
  }

  updateVisiblePages() {
    const startPage = Math.max(1, this.currentPage - 1);
    const endPage = Math.min(startPage + 2, this.totalPages);

    this.visiblePages = Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);
    
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }
}
