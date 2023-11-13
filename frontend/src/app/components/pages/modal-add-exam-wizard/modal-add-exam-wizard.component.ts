import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IdName, TeacherExam } from 'src/app/shared/models/exams';
import { UsersMe } from 'src/app/shared/models/users';
import { HttpParams } from '@angular/common/http';
import { Subject, takeUntil } from 'rxjs';
import { TeacherService } from 'src/app/shared/service/teacher.service';
import { TeacherClassroom } from 'src/app/shared/models/classrooms';
import { UsersService } from 'src/app/shared/service/users.service';

@Component({
  selector: 'app-modal-add-exam-wizard',
  templateUrl: './modal-add-exam-wizard.component.html',
  styleUrls: ['./modal-add-exam-wizard.component.scss']
})
export class ModalAddExamWizardComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<ModalAddExamWizardComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: any,
    private examsService: ExamsService,
    private teacherService: TeacherService,
    private usersService: UsersService){}
    

  examsList!: TeacherExam[];
  subjects!: IdName[]
  classrooms!: TeacherClassroom[]
  examId?: string
  subjectsByClassroom?: IdName[]
  user!: UsersMe
  isEdit = this.data
  examForm!: FormGroup;
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
  currentDate = new Date()
  today = new Date(new Date().getTime()).toISOString().substring(0,10);
  unsubscribe$: Subject<boolean> = new Subject<boolean>();


  ngOnInit(): void {
    this.examForm = new FormGroup({
    date : new FormControl(null, Validators.required),
    classroomId : new FormControl(null, Validators.required),
    subjectId : new FormControl(null, Validators.required)
  });
      this.getUser();
      this.getTeacherClassrooms();
  }

  compileForm(){
    this.examForm.patchValue({
      date: this.data.exam?.date ?? null,
      classroomId: this.data.exam?.classroom.id ?? this.data.teacher?.classroomId,
      subjectId: this.data.exam?.subject.id ?? this.data?.teacher?.subjectId
    });
    this.getTeacherSubjectsByClassroom(this.examForm.value.classroomId)
    
  }


    getTeacherClassrooms() {
      this.teacherService.getDataClassroom().pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: (res) => {
          this.classrooms = res.data;
          
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

    getUser() {
      this.usersService.getUsersMe().pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: (res) => {
          this.user = res
          this.getTeacherSubjects();
          if(this.data.type){
            this.compileForm();
           }
          console.log("DEBUG USER ", this.user);
        }
      })
    }
  
    getTeacherSubjectsByClassroom(classroomId: string) {
      const params = new HttpParams().set('classroomId', classroomId)
      console.log(params)
      this.teacherService.getTeacherSubjectsByClassroom(this.user?.id, params).pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: (res: IdName[]) => {
          this.subjectsByClassroom = res;
        }
      })
    }
  
    subjectEvent(event: any) {
      this.getTeacherSubjectsByClassroom(event.target.value)
    }

    //funzione per creazione o modifica esame
    onClickModal() {
      if (this.data.type === 'add' || !(this.data.type)) {
       
        this.examsService.addExam(this.examForm.value).pipe(takeUntil(this.unsubscribe$)).subscribe({
          next: () => {
            this.examForm.reset();
          }
        })
      }
      else {
        this.examsService.editExam(this.examForm.value, this.user.id, this.data.exam.id).pipe(takeUntil(this.unsubscribe$)).subscribe({
          next: () => {
          }
        })    
      }
    }


    onCloseModal(){
      this.dialogRef.close();
    }

}
