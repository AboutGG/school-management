export interface TeacherExam {
  id?: string,
  date: string,
  classroom: {
    id: string,
    name: string
  },
  subject: {
    id: string,
    name: string
  }
}