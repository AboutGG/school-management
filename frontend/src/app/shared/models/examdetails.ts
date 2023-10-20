export interface ExamStudentDetails {
	studentId: string,
	grade: number
}

export interface ExamDetails {
	examDate: string,
  subject: string,
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