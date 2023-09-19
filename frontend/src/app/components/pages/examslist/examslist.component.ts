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

  formSubject = new FormGroup({
    subjects: new FormControl('')
  })
  formClassroom = new FormGroup({
    classrooms: new FormControl('')
  });
  classOrSubjs!: string
  examsList!: TeacherExam[]
  subject!: string
  subjects: string[] = []
  teacher!: Teacher
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
  itemsPerPage: number = 50
  onClickClassroom: boolean = false
  onClickSubject: boolean = false
  onClickFilter: boolean = false
  switchText: string = "Vedi classi"

  ngOnInit(): void {
    this.getTeacherExams();
    this.getTeacherClassrooms();
    this.getTeacherSubjects();
  }

  resetForm() {
    return this.onClickFilter === true ? this.formClassroom.reset() : this.formSubject.reset();
  }

  getTeacherExams(): void {
    this.examsService.getTeacherExams(this.page, this.filtered, this.search, this.orderType, this.order, this.itemsPerPage).subscribe({
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
        data.subjects.map(subjects => {
          this.subjects.push(subjects.subjectName)
        })
      }
    });
  }

  filterSwitch() {
    if (this.onClickFilter) {
      this.onClickFilter = false;
      this.subject = this.filtered
      this.switchText = "Vedi classi"
      
    } else {
      this.onClickFilter = true
      this.classroom = this.filtered
      this.switchText = "Vedi materie"

    }
  }

  dropdownFilter(filtered: string) {
    if (this.onClickFilter === false) {
      this.subject = filtered
    } else {
      this.classroom = filtered
    }
    this.filtered = filtered
  }
  // getTeacherSubjects() {
  //   this.teacherService.getTeacherSubjects().subscribe({
  //     next: (data) => {
  //       this.teacher = data
  //       this.subject = data.subjects.
  //     }
  //   })
  // }

  // filterBySubject(subject: string) {
  //   this.subject = subject;
  // }

  // filterByClassroom(classroom: string) {
  //   this.classroom = classroom;
  // }

  // dropdownFilter(filtered: string) {
  //   if (this.onClickClassroom) {
  //     this.onClickSubject = false
  //     this.classroom = filtered
  //     this.subject = ""
  //   }
  //   if (this.onClickSubject) {
  //     this.onClickClassroom = false
  //     this.subject = filtered
  //     this.classroom = ""
  //   }
  //   if (!this.onClickClassroom && !this.onClickSubject) {

  //   }
  // }
  // getExamData(order: string, type: string, matter: string) {
  //   this.usersService.getMatter(order, type, matter).subscribe({
  //     next: (data: Registry[]) => {
  //       this.users = data
  //     },
  //     error: (error: any) => {
  //       console.log(error);
  //     }
  //   });
  // }

}