import { Component } from '@angular/core';
import { StudentExams } from 'src/app/shared/models/users';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { UsersService } from 'src/app/shared/service/users.service';

@Component({
  selector: 'app-exams-student-list',
  templateUrl: './exams-student-list.component.html',
  styleUrls: ['./exams-student-list.component.scss']
})
export class ExamsStudentListComponent {
  examsList!: StudentExams
  orders = {
    date: 'asc',
    matter: 'asc',
    grade: 'asc'
  }
  constructor(private examsService: ExamsService) { }

  getExams(order: string, type: 'asc' | 'desc', key: keyof typeof this.orders) {
    this.examsService.getExams(order, type, key).subscribe({
      next: (data: StudentExams) => {
        this.orders = {
          date: 'desc',
          matter: 'asc',
          grade: 'desc',
          [key]: type
        }
        this.examsList = data
      }
    })
  }

}
