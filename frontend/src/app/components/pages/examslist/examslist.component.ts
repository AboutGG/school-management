import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Teacher, TeacherExam } from 'src/app/shared/models/users';
import { ClassroomService } from 'src/app/shared/service/classroom.service';
import { ExamsService } from 'src/app/shared/service/exams.service';
import { TeacherService } from 'src/app/shared/service/teacher.service';

@Component({
  selector: 'app-examslist',
  templateUrl: './examslist.component.html',
  styleUrls: ['./examslist.component.scss']
})
export class ExamslistComponent {
  constructor(private examsService: ExamsService, private teacherService: TeacherService, private classroomService: ClassroomService) { }

  formSubjects = new FormGroup({
    subjects: new FormControl('')
  })
  formClassrooms = new FormGroup({
    classrooms: new FormControl('')
  });
  examsList!: TeacherExam[]
  subject!: string
  subjects: string[] = []
  classroom!: string
  classrooms: string[] = []
  orders = {
    date: 'asc',
    subject: 'asc',
    classroom: 'asc'
  }
  page: number = 1
  filtered: string = ""
  search: string = ""
  orderType: string = "asc"
  order: string = "examDate"
  itemsPerPage: number = 10
  onClickFilter: boolean = false

  ngOnInit(): void {
    this.getTeacherExams();
    this.getTeacherClassrooms();
    this.getTeacherSubjects();
  }

  resetAllDropdown() {
    [this.formClassrooms.reset({classrooms: ""})] && [this.formSubjects.reset({subjects: ""})]
    this.getTeacherExams()
  }
  dropdownFilter() {
    this.onClickFilter === true ? [this.formClassrooms.reset({ classrooms: "" })] && [this.filtered = this.formSubjects.value.subjects as string] : [this.formSubjects.reset({ subjects: "" })] && [this.filtered = this.formClassrooms.value.classrooms as string];
    this.getTeacherExams()
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
      next: (data: TeacherExam[]) => {
        // this.orders = {
        //   name: 'asc',
        //   surname: 'asc',
        //   birth: 'asc',
        //   [id]: type
        // };
        // id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
        // id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc"; 
        // id === 'birth' && this.orderBirth === "desc" ? this.orderBirth = "asc" : this.orderBirth = "desc";
        this.examsList = data
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  getTeacherClassrooms() {
    this.teacherService.getDataClassroom().subscribe({
      next: (data) => {
        data.map(classroom => {
          this.classrooms.push(classroom.name_classroom);
        })
      }
    });
  }

  getTeacherSubjects() {
    this.teacherService.getTeacherSubjects().subscribe({
      next: (data) => {
        data.map(subjects => {
          this.subjects.push(subjects.subjectName)
        })
      }
    });
  }

}