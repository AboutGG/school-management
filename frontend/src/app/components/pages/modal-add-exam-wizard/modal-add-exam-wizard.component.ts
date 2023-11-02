import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ExamslistComponent } from '../examslist/examslist.component';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TeacherExam } from 'src/app/shared/models/teacherexam';
import { ListResponse } from 'src/app/shared/models/users';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-modal-add-exam-wizard',
  templateUrl: './modal-add-exam-wizard.component.html',
  styleUrls: ['./modal-add-exam-wizard.component.scss']
})
export class ModalAddExamWizardComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<ModalAddExamWizardComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private examsService: ExamsService){}


  examsList!: TeacherExam[];
  examForm!: FormGroup;
  classroomId!: FormControl
  subjectId!: FormControl
  date!: FormControl
  total!: number
  page: number = 1
  itemsPerPage: number = 10
  filtered: string = ""
  search: string = ""
  orderType: string = "asc"
  order: string = "Date"
  onClickFilter: boolean = false
  totalPages!: number
  selectedPages!: number

  ngOnInit(): void {
    this.date = new FormControl(null, Validators.required),
    this.classroomId = new FormControl(null, Validators.required),
    this.subjectId = new FormControl(null, Validators.required)

  this.examForm = new FormGroup({
    date: this.date,
    classroomId: this.classroomId,
    subjectId: this.subjectId
  });
      console.log(this.data);
      
  }

  onCloseModal(){
    this.dialogRef.close()

  }

  onSaveClick() {
    this.examsService.addExam(this.examForm.value).subscribe({
      next: () => {
        this.getTeacherExams()
        
      }
    })
     this.dialogRef.close()
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
          this.examsList = res.data
          this.total = res.total
        },
        error: (error) => {
          console.log(error);
        }
      });
    }


}
