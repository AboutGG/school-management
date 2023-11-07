import { Location } from '@angular/common';
import { HttpParams } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { StudentExam } from 'src/app/shared/models/exams';
import { ExamsService } from 'src/app/shared/service/exams.service';

@Component({
  selector: 'app-exams-student-list',
  templateUrl: './exams-student-list.component.html',
  styleUrls: ['./exams-student-list.component.scss']
})
export class ExamsStudentListComponent implements OnInit, OnDestroy {
  examsList!: StudentExam[]
  page: number = 1;
  filtered!: string
  search!: string
  orderType: string = 'desc'
  order: string = 'Date'
  itemsPerPage: number = 10
  orders = {
    date: 'asc',
    matter: 'asc',
    grade: 'asc'
  }
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  constructor(private examsService: ExamsService, private location: Location) { }

  ngOnInit(): void {
    this.getStudentExams()
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }

  // getExams(order: string, type: 'asc' | 'desc', key: keyof typeof this.orders) {
  //   this.examsService.getExams(order, type, key).subscribe({
  //     next: (data: StudentExams) => {
  //       this.orders = {
  //         date: 'desc',
  //         matter: 'asc',
  //         grade: 'desc',
  //         [key]: type
  //       }
  //       this.examsList = data
  //       console.log("CIAOOOO" + this.examsList);
  //     }
  //   })
  // }

  getStudentExams() {
    const params = new HttpParams()
      .set('Page', this.page)
      .set('Search', this.search)
      .set('OrderType', this.orderType)
      .set('Order', this.order)
      .set('ItemsPerPage', this.itemsPerPage)
    this.examsService.getStudentExams(params).pipe(takeUntil(this.unsubscribe$)).subscribe(res => {
      this.examsList = res.data
    })
  }

  goBack() {
    this.location.back();
  }

}
