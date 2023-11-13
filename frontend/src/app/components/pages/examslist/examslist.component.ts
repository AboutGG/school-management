import { UsersService } from "../../../shared/service/users.service";
import { HttpParams } from "@angular/common/http";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { TeacherClassroom } from "src/app/shared/models/classrooms";
import { ListResponse } from "src/app/shared/models/listresponse";
import { IdName, TeacherExam } from "src/app/shared/models/exams";
import { ExamsService } from "src/app/shared/service/exams.service";
import { TeacherService } from "src/app/shared/service/teacher.service";
import { UsersMe } from "src/app/shared/models/users";
import Swal from "sweetalert2";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { Subject, takeUntil } from "rxjs";
import { Location } from "@angular/common";
import { ModalAddExamWizardComponent } from "../modal-add-exam-wizard/modal-add-exam-wizard.component";

@Component({
  selector: "app-examslist",
  templateUrl: "./examslist.component.html",
  styleUrls: ["./examslist.component.scss"],
})
export class ExamslistComponent implements OnInit, OnDestroy {
  constructor(
    private examsService: ExamsService,
    private teacherService: TeacherService,
    private usersService: UsersService,
    private location: Location,
    public dialog: MatDialog
  ) {}

  user!: UsersMe;
  formSubjects = new FormGroup({
    subjects: new FormControl(""),
  });
  formClassrooms = new FormGroup({
    classrooms: new FormControl(""),
  });
  examsList!: TeacherExam[];
  subjects!: IdName[];
  classrooms!: TeacherClassroom[];
  currentPage: number = 1;
  itemsPerPage: number = 5;
  filtered: string = "";
  search: string = "";
  orderType: string = "asc";
  order: string = "Date";
  onClickFilter: boolean = false;
  totalItems!: number;
  totalPages!: number;
  selectedPages!: number;
  total!: number;
  examId?: string;
  currentDate = new Date();
  today = new Date(new Date().getTime()).toISOString().substring(0, 10);
  alert: boolean = false;
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  ngOnInit(): void {
    this.getUser();
    this.getTeacherExams();
    this.getTeacherClassrooms();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }

  onChangePage(newPage: number) {
    this.currentPage = newPage;
    this.getTeacherExams();
    console.log(this.currentPage);
  }

  dropdownFilter() {
    this.onClickFilter === true
      ? [this.formClassrooms.reset({ classrooms: "" })] && [
          (this.filtered = this.formSubjects.value.subjects as string),
        ]
      : [this.formSubjects.reset({ subjects: "" })] && [
          (this.filtered = this.formClassrooms.value.classrooms as string),
        ];
    this.getTeacherExams();
  }

  getUser() {
    this.usersService.getUsersMe().subscribe({
      next: (res) => {
        this.user = res;
        this.getTeacherSubjects();
      },
    });
  }

  getTeacherExams() {
    const params = new HttpParams()
      .set("Page", this.currentPage)
      .set("Filter", this.filtered)
      .set("Search", this.search)
      .set("OrderType", this.orderType)
      .set("Order", this.order)
      .set("ItemsPerPage", this.itemsPerPage);
    this.examsService.getTeacherExams(params).subscribe({
      next: (res: ListResponse<TeacherExam[]>) => {
        this.examsList = res.data;
        this.totalItems = res.total;
        this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getTeacherClassrooms() {
    this.teacherService.getDataClassroom().subscribe({
      next: (res) => {
        this.classrooms = res.data;
      },
    });
  }

  getTeacherSubjects() {
    this.teacherService.getTeacherSubjects(this.user?.id).subscribe({
      next: (res) => {
        this.subjects = res;
      },
    });
  }

  onDelete(id: string) {
    this.examId = id;
  }

  deleteExam() {
    this.examsService.deleteExam(this.examId!).subscribe({
      next: () => {
        this.getTeacherExams();
        Swal.fire({
          toast: true,
          position: "top-end",
          icon: "success",
          title: "Esame eliminato con successo",
          showConfirmButton: false,
          timer: 2500,
          background: "#fce6a4",
        });
      },
    });
  }

  goBack() {
    this.location.back();
  }

  showAlert(action: string) {
    let title = "";
    if (action === "create") {
      title = "Esame creato con successo";
    } else if (action === "edit") {
      title = "Esame modificato con successo";
    }

    Swal.fire({
      toast: true,
      position: "top-end",
      icon: "success",
      title: title,
      showConfirmButton: false,
      timer: 2500,
      background: "#fce6a4",
    });
  }

  // Open Modal Componente padre
  openModalExam(exam?: TeacherExam, type?: string) {
    const dialogRef = this.dialog.open(ModalAddExamWizardComponent, {
      width: "400px",
      height: "400px",
      data: { exam, type },
    });
    dialogRef.beforeClosed().subscribe((result: any) => {
      this.getTeacherExams();
      this.showAlert(type === "edit" ? "edit" : "create");
    });
    dialogRef.afterClosed().subscribe((result: any) => {
      dialogRef.close();
    });
  }
}
