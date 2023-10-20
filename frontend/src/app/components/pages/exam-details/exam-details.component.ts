import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ExamDetails, ExamStudentDetails } from 'src/app/shared/models/examdetails';
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
  

  constructor(private examService: ExamsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.examId = params['id'];
    this.getExamDetails();
    this.grade = new FormControl(null, Validators.required)
    })
  }

  getExamDetails() {
    this.examService.getExamDetails(this.examId).subscribe({
      next: (res: ExamDetails) => {
        this.examDetails = res
      }
    })
  }

  editExamDetails(studentId: string) {
    this.studentDetails = {
      studentId : "8767fd02-7891-4b47-8b02-3cc0d07ac334",
      grade : this.grade.value
     }
    console.log(this.examId);
    
    this.examService.editExamDetails(this.studentDetails, this.examId).subscribe({
      next: (res: ExamDetails) => {
        console.log(res);
        
      }
    })
  }

}
