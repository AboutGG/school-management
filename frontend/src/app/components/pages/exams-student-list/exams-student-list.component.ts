import { Component, OnInit } from '@angular/core';
import { StudentExams } from 'src/app/shared/models/users';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ExamsService } from 'src/app/shared/service/exams.service';

@Component({
  selector: 'app-exams-student-list',
  templateUrl: './exams-student-list.component.html',
  styleUrls: ['./exams-student-list.component.scss']
})
export class ExamsStudentListComponent implements OnInit {
  examsList!: StudentExams
  orders = {
    date: 'asc',
    matter: 'asc',
    grade: 'asc'
  }

  role = this.authService.getRole()
  constructor(private examsService: ExamsService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getExams()
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

  getExams() {
    this.examsService.getStudentExams().subscribe(data => {
      this.examsList = data
      console.log(data);
    })
  }

}
