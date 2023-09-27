import { TeacherSubject } from './../../../shared/models/subjects';
import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Classroom } from 'src/app/shared/models/classrooms';
import { ListResponse } from 'src/app/shared/models/listResponse';
import { TeacherExam } from 'src/app/shared/models/teacherexam';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { TeacherService } from 'src/app/shared/service/teacher.service';

@Component({
  selector: 'app-examslist',
  templateUrl: './examslist.component.html',
  styleUrls: ['./examslist.component.scss']
})
export class ExamslistComponent {
  constructor(private examsService: ExamsService, private teacherService: TeacherService) { }

  formSubjects = new FormGroup({
    subjects: new FormControl('')
  })
  formClassrooms = new FormGroup({
    classrooms: new FormControl('')
  });
  examsList!: TeacherExam[]
  subject!: string
  subjects: TeacherSubject[] = []
  subjectsName: string[] = []
  classroom!: string
  classrooms: Classroom[] = []
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
  order: string = "Id"
  onClickFilter: boolean = false
  totalPages!: number
  selectedPages!: number
  total!: number

  ngOnInit(): void {
    this.getTeacherExams();
    this.getTeacherClassrooms();
    this.getTeacherSubjects();
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

  getTeacherExams() {
    const params = new HttpParams()
      .set('Page', this.page)
      .set('Filter', this.filtered)
      .set('Search', this.search)
      .set('OrderType', this.orderType)
      .set('Order', this.order)
      .set('ItemsPerPage', this.itemsPerPage)
    this.examsService.getTeacherExams(params).subscribe({
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
    this.teacherService.getDataClassroom().subscribe({
      next: (res) => {
        this.classrooms = res.data        
      }
    });
  }

  // getTeacherClassrooms() {
  //   this.teacherService.getDataClassroom().subscribe({
  //     next: (res) => {
  //       console.log(res);
  //     }
  //   });
  // }

  getTeacherSubjects() {
    this.teacherService.getTeacherSubjects().subscribe({
      next: (res) => {
        this.subjects = res.data
      }
    });
  }

}