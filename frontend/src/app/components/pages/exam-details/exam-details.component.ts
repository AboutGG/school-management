import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ExamDetails, ExamStudentDetails } from 'src/app/shared/models/examdetails';
import { AuthService } from 'src/app/shared/service/auth.service';
import { ExamsService } from 'src/app/shared/service/exams.service';

@Component({
  selector: 'app-exam-details',
  templateUrl: './exam-details.component.html',
  styleUrls: ['./exam-details.component.scss']
})
export class ExamDetailsComponent implements OnInit {

  examDetails!: ExamDetails
  examId!: string
  studentDetails!: ExamStudentDetails
  grade!: FormControl
  currentDate = new Date()
  today = this.currentDate.getFullYear() + "-" + (this.currentDate.getMonth() + 1) + "-" + this.currentDate.getDate();
  
  constructor(private examService: ExamsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.examId = params['id'];
      this.grade = new FormControl(null, Validators.required)
      this.getExamDetails();
    })
  }

  getExamDetails() {
    this.examService.getExamDetails(this.examId).subscribe({
      next: (res: ExamDetails) => {
        this.examDetails = res
      }
    })
  }

  editExamDetails(userId: string) {
    if(this.grade.value >= 2 && this.grade.value <= 10) {
      this.studentDetails = {
        userId : userId,
        grade : this.grade.value
       }
      this.examService.editExamDetails(this.studentDetails, this.examId).subscribe()
      this.getExamDetails()
    }
    else {
      alert("Impostare un valore da 2 a 10")
    }
  }

}
