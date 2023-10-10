import { FormControl } from "@angular/forms"

export interface TeacherExam {
  id?: string,
  date: string,
  classroom: Subject
  subject: Subject
}

export interface Subject {
  id: string,
  name: string
}