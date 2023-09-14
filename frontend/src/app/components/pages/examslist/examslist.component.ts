import { Component } from '@angular/core';
import { Classroom, Teacher, TeacherExams, TeacherSubjects, Teachers } from 'src/app/shared/models/users';
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

  examsList!: TeacherExams[]
  subject!: string
  filteredSubject!: string
  subjects: string[] = []
  // teacherSubjects!: TeacherSubjects[]
  teacher!: Teacher
  classroom!: string
  classrooms: string[] = []
  orders = {
    name: 'asc',
    surname: 'asc',
    birth: 'asc'
  }
  
  ngOnInit(): void {
    this.getTeacherExams();
    this.getTeacherClassrooms();
    this.getTeacherSubjects();
  }

  getTeacherExams(): void {
    this.examsService.getTeacherExams().subscribe({
      next: (data: TeacherExams[]) => {
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
        data.map(classroom =>{
          this.classrooms.push(classroom.name_classroom);
        })
      }
    });
  }

  getTeacherSubjects() {
    this.teacherService.getTeacherSubjects().subscribe({
      next: (data) => {
        data.subjects.map(subjects => {
          this.subjects.push(subjects.subject.name)
        })
      }
    });
  }

  filterBySubject(subject: string): void {
    this.subject = subject;
  }

  filterByClassroom(classroom: string): void {
    this.classroom = classroom;
  }
  

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
