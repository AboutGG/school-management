import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ExamDetails, ExamStudentDetails } from 'src/app/shared/models/exams';
import { ExamsService } from 'src/app/shared/service/exams.service';

@Component({
  selector: 'app-exam-details',
  templateUrl: './exam-details.component.html',
  styleUrls: ['./exam-details.component.scss']
})
export class ExamDetailsComponent implements OnInit, OnDestroy {

  examDetails!: ExamDetails
  examId!: string
  studentDetails!: ExamStudentDetails
  grade!: FormControl
  currentDate = new Date()
  today = this.currentDate.getFullYear() + "-" + (this.currentDate.getMonth() + 1) + "-" + this.currentDate.getDate();
  unsubscribe$: Subject<boolean> = new Subject<boolean>();
  
  constructor(private examService: ExamsService, private route: ActivatedRoute, private location: Location) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.examId = params['id'];
      this.grade = new FormControl(null, Validators.required)
      this.getExamDetails();
    })
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete();
  }

  getExamDetails() {
    this.examService.getExamDetails(this.examId).pipe(takeUntil(this.unsubscribe$)).subscribe({
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
      this.examService.editExamDetails(this.studentDetails, this.examId).pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: () => {
          this.getExamDetails()
        }
      })

    }
    else {
      alert("Impostare un valore da 2 a 10")
    }
  }

  goBack() {
    this.location.back();
  }

}
