import { UsersService } from './../../../shared/service/users.service';
import { HttpParams } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TeacherClassroom } from 'src/app/shared/models/classrooms';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { IdName, TeacherExam } from 'src/app/shared/models/teacherexam';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { TeacherService } from 'src/app/shared/service/teacher.service';
import { UsersMe } from 'src/app/shared/models/users';
import { Subject, takeUntil } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-examslist',
  templateUrl: './examslist.component.html',
  styleUrls: ['./examslist.component.scss']
})
export class ExamslistComponent implements OnInit, OnDestroy {
  constructor(private examsService: ExamsService, private teacherService: TeacherService, private usersService: UsersService, private location: Location) { }

  user!: UsersMe
  formSubjects = new FormGroup({
    subjects: new FormControl('')
  })
  formClassrooms = new FormGroup({
    classrooms: new FormControl('')
  });
  examsList!: TeacherExam[]
  subjects!: IdName[]
  classrooms!: TeacherClassroom[]
  orders = {
    date: 'asc',
    subject: 'asc',
    classroom: 'asc'
  }
  page: number = 1
  itemsPerPage: number = 10
  filtered: string = ""
  search: string = ""
  orderType: string = "asc"
  order: string = "Date"
  onClickFilter: boolean = false
  totalPages!: number
  selectedPages!: number
  total!: number
  isEdit: boolean = false
  successEditOrNew: boolean = false
  examId?: string
  subjectsByClassroom?: IdName[]
  classroomId!: FormControl
  subjectId!: FormControl
  date!: FormControl
  examForm!: FormGroup
  currentDate = new Date()
  today = this.currentDate.getFullYear() + "-" + (this.currentDate.getMonth() + 1) + "-" + this.currentDate.getDate();
  // today = new Date(new Date().getTime()).toISOString().substring(0, 10);
  alert: boolean = false;
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  ngOnInit(): void {
    this.date = new FormControl(null, Validators.required),
      this.classroomId = new FormControl(null, Validators.required),
      this.subjectId = new FormControl(null, Validators.required)

    this.examForm = new FormGroup({
      date: this.date,
      classroomId: this.classroomId,
      subjectId: this.subjectId
    });

    this.getUser();
    this.getTeacherExams();
    this.getTeacherClassrooms();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }  

  onChangePage(newPage: number) {
    this.page = newPage
    this.getTeacherExams()
    this.getTeacherClassrooms()
    this.getTeacherSubjects()
  }

  dropdownFilter() {
    this.onClickFilter === true ? [this.formClassrooms.reset({ classrooms: "" })] && [this.filtered = this.formSubjects.value.subjects as string] : [this.formSubjects.reset({ subjects: "" })] && [this.filtered = this.formClassrooms.value.classrooms as string];
    this.getTeacherExams()
  }

  getUser() {
    this.usersService.getUsersMe().pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res) => {
        this.user = res
        this.getTeacherSubjects();
        console.log("DEBUG USER ", this.user);
      }
    })
  }

  getTeacherExams() {
    const params = new HttpParams()
      .set('Page', this.page)
      .set('Filter', this.filtered)
      .set('Search', this.search)
      .set('OrderType', this.orderType)
      .set('Order', this.order)
      .set('ItemsPerPage', this.itemsPerPage)
    this.examsService.getTeacherExams(params).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res: ListResponse<TeacherExam[]>) => {
        // this.orders = {
        //   name: 'asc',
        //   surname: 'asc',
        //   birth: 'asc',
        //   [id]: type
        // };
        // id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
        // id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc"; 
        // id === 'birth' && this.orderBirth === "desc" ? this.orderBirth = "asc" : this.orderBirth = "desc";
        this.examsList = res.data
        this.total = res.total
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  getTeacherClassrooms() {
    this.teacherService.getDataClassroom().pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res) => {
        this.classrooms = res.data
      }
    });
  }

  getTeacherSubjects() {
    this.teacherService.getTeacherSubjects(this.user?.id).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res) => {
        this.subjects = res
        console.log(res);
      }
    });
  }

  getTeacherSubjectsByClassroom(classroomId: string) {
    // const params = classroomId ? new HttpParams().set('classroomId', classroomId) : new HttpParams();
    const params = new HttpParams().set('classroomId', classroomId)
    console.log(params)
    this.teacherService.getTeacherSubjectsByClassroom(this.user?.id, params).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res: IdName[]) => {
        this.subjectsByClassroom = res;
      }
    })
  }

  editExam(exam: TeacherExam) {
    this.examId = exam.id
    this.examForm.patchValue({
      date: exam.date,
      classroomId: exam.classroom.id,
      subjectId: exam.subject.id
    })
    this.getTeacherSubjectsByClassroom(this.examForm.value.classroomId)
  }

  subjectEvent(event: any) {
    this.getTeacherSubjectsByClassroom(event.target.value)
  }

  onClickModal() {
    if (this.isEdit === false) {
      if (this.examForm.value.date > this.today) {
        this.successEditOrNew = false;
        this.examsService.addExam(this.examForm.value).pipe(takeUntil(this.unsubscribe$)).subscribe({
          next: () => {
            this.successEditOrNew = true;
            setTimeout(() => this.successEditOrNew = false, 4000)
            this.examForm.reset();
            this.getTeacherExams()
          }
        })
      } else {
        alert("Selezionare una data successiva a quella odierna");
      }

    } else {
      if (this.examForm.value.date > this.today) {
        this.successEditOrNew = false
        this.examsService.editExam(this.examForm.value, this.user.id, this.examId).pipe(takeUntil(this.unsubscribe$)).subscribe({
          next: () => {
            this.successEditOrNew = true
            setTimeout(() => this.successEditOrNew = false, 4000)
            this.getTeacherExams()
          }
        })
      } else {
        alert("Selezionare una data successiva a quella odierna");
      }
    }

  }

  onDelete(id: string) {
    this.examId = id;
  }

  deleteExam() {
    this.examsService.deleteExam(this.examId!).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: () => {
        this.getTeacherExams();
      }
    })
  }

  goBack() {
    this.location.back();
  }

}