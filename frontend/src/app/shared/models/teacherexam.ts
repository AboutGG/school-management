export interface TeacherExam {
  id?: string,
  date: string,
  classroom: {
    id: string,
    name: string
  },
  subject: Subject
}

export interface Subject {
  id: string,
  name: string,
}

