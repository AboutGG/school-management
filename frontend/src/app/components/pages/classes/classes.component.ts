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

  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  constructor(private classroomService: ClassroomService) {}

  ngOnInit() {
    this.fetchData();
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
          this.totalPages = this.totalItems / this.itemsPerPage;
          this.class = res.data;

          console.log("dati get", res.data);
          console.log("params", params);
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
    this.fetchData();
    console.log("page", this.currentPage);
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }
}
