import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { TeacherSubject } from 'src/app/shared/models/subjects';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalAddExamWizardComponent } from '../modal-add-exam-wizard/modal-add-exam-wizard.component';
import Swal from 'sweetalert2';

@Component({
  selector: "app-subjects",
  templateUrl: "./subjects.component.html",
  styleUrls: ["./subjects.component.scss"]
})
export class SubjectsComponent {
  teachers: TeacherSubject[] = [];
  searchTerm: string = "";
  currentPage: number = 1;
  itemsPerPage: number = 5; // numero di elementi per pagina
  totalItems!: number;
  newPage!: string;
  previousPage: number = 1;
  totalPages!: number;
  order: string = 'Classroom.Name';
  alert: boolean = false;


  constructor(private classroomService: ClassroomService, public dialog: MatDialog) {}

  ngOnInit() {
    this.fetchData();
  }

  // get dati api teacher subjects;
  fetchData() {
    const params = new HttpParams()
      .set('Page', this.currentPage)
      .set('Search', this.searchTerm)
      .set('Order', this.order)
      .set('ItemsPerPage', this.itemsPerPage);

    this.classroomService.getTeacherSubjects(params).subscribe({
      next: (res: ListResponse<TeacherSubject[]>) => {
        this.totalItems = res.total; // numero totale di elementi;
        this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
        this.teachers = res.data;
      },
      error: (err) => {
        console.log("error", err);
      },
    });
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
  }

   // Open Modal Componente padre
  openModalExam(teacher: TeacherSubject, type?: string) {
    const dialogRef = this.dialog.open(ModalAddExamWizardComponent, {
      width: '400px',
      height: '400px',
      data: { teacher, type }
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      dialogRef.close();
      this.showAlert('add');
    });
  }

  showAlert(action: string) {
    let title = "";
    if (action === "add") {
      title = "Esame creato con successo";
    }

    Swal.fire({
      toast: true,
      position: "top-end",
      icon: "success",
      title: title,
      showConfirmButton: false,
      timer: 2500,
      background: "#fce6a4"
     
    });
  }
}
