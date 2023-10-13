export interface TeacherExam {
  id?: string,
  date: string,
  classroom: IdName
  subject: IdName
}

export interface IdName {
  id: string,
  name: string
}