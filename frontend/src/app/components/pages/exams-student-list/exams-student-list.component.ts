import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { StudentExam } from 'src/app/shared/models/users';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ExamsService } from 'src/app/shared/service/exams.service';

@Component({
  selector: 'app-exams-student-list',
  templateUrl: './exams-student-list.component.html',
  styleUrls: ['./exams-student-list.component.scss']
})
export class ExamsStudentListComponent implements OnInit {
  examsList!: StudentExam[]
  page: number = 1;
  filtered!: string
  search!: string
  orderType!: string
  order!: string
  itemsPerPage!: number
  orders = {
    date: 'asc',
    matter: 'asc',
    grade: 'asc'
  }

  role = this.authService.getRole()
  constructor(private examsService: ExamsService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getStudentExams()
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
      .set('Filter', this.filtered)
      .set('Search', this.search)
      .set('OrderType', this.orderType)
      .set('Order', this.order)
      .set('ItemsPerPage', this.itemsPerPage)
    this.examsService.getStudentExams().subscribe(res => {
      this.examsList = res.data
    })
  }

}
