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

export interface StudentExam {
  subject: string,
  teacher: string,
  date: string,
  grade?: number
}

export interface ExamStudentDetails {
	userId: string,
	grade: number
}

export interface ExamDetails {
	examDate: string,
  subject: string,
  classroom: string,
  studentExams: [
    {
      student: {
        id: string,
        name: string,
        surname: string
      },
      grade: number
    }
  ]
}