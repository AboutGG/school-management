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