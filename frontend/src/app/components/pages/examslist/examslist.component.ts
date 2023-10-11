import { UsersService } from './../../../shared/service/users.service';
import { TeacherSubject } from './../../../shared/models/subjects';
import { HttpParams } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TeacherClassroom } from 'src/app/shared/models/classrooms';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { IdName, TeacherExam } from 'src/app/shared/models/teacherexam';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { TeacherService } from 'src/app/shared/service/teacher.service';
import { UsersMe } from 'src/app/shared/models/users';

@Component({
  selector: 'app-examslist',
  templateUrl: './examslist.component.html',
  styleUrls: ['./examslist.component.scss']
})
export class ExamslistComponent {
  constructor(private examsService: ExamsService, private teacherService: TeacherService, private usersService: UsersService) { }

  user!: UsersMe
  formSubjects = new FormGroup({
    subjects: new FormControl('')
  })
  formClassrooms = new FormGroup({
    classrooms: new FormControl('')
  });
  examsList!: TeacherExam[]
  subjects!: TeacherSubject[]
  classrooms!: TeacherClassroom[]
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
  order: string = "Date"
  onClickFilter: boolean = false
  totalPages!: number
  selectedPages!: number
  total!: number
  isEdit: boolean = false
  examId?: string
  subjectsByClassroom?: IdName[]
  classroomId!: FormControl
  subjectId!: FormControl
  date!: FormControl
  examForm!: FormGroup

  ngOnInit(): void {
    this.date = new FormControl(null, Validators.required),
    this.classroomId = new FormControl(null, Validators.required),
    this.subjectId = new FormControl(null, Validators.required)

    this.examForm = new FormGroup({
      date: this.date,
      classroomId: this.classroomId,
      subjectId: this.subjectId
    });

    this.getUser();
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

  getUser() {
    this.usersService.getUsersMe().subscribe({
      next: (res) => {
        this.user = res
        console.log("DEBUG USER ", this.user);
      }
    })
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

  getTeacherSubjects() {
    this.teacherService.getTeacherSubjects().subscribe({
      next: (res) => {
        this.subjects = res.data
      }
    });
  }

  editExam(exam: TeacherExam) {
    this.examId = exam.id
    this.examForm.patchValue({
      date: exam.date,
      classroomId: exam.classroom.id,
      subjectId: exam.subject.id
    })
    this.getTeacherSubjectByClassroom(this.examForm.value.classroomId)
  }

  getTeacherSubjectByClassroom(classroomId: string){
    console.log(classroomId);
    const params = new HttpParams().set('classroomId', classroomId);
    this.teacherService.getTeacherSubjectByClassroom(this.user.id, params).subscribe({
      next: (res: IdName[]) => {
        this.subjectsByClassroom = res;
        console.log("SONO RES ", res);
      }
    })
  }

  onClickModal() { 
    if (this.isEdit === false) {
      console.log("PRIMA ADD: ", this.examForm.value);
      this.examsService.addExam(this.examForm.value).subscribe({
        next: () => {          
          this.getTeacherExams()
          console.log("DOPO ADD ",this.examForm.value);
        }
      });
    } else {
      console.log("PRIMA EDIT: ", this.examForm.value);
      this.examsService.editExam(this.examForm.value, this.user.id, this.examId).subscribe({
        next: () => {
          this.getTeacherExams()
          console.log("DOPO EDIT ",this.examForm.value);
        }
      })
    }
  }

}