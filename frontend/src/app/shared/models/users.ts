export interface Users {
  classroom?: string
  user: User
  registry: Registry
}



export interface User {
  id?: string
  username: string
  password: string
}

export interface TypeCount {
  Users: number,
  Students: number,
  Teachers: number,
  Classrooms: number
}

export interface Registry {
  id?: string
  name: string
  surname: string
  gender: string
  birth?: string
  email?: string
  address?: string
  telephone?: string //date format "yyyy-mm-dd"
}

export interface StudentExams {
  id: string,
  classroomId: string,
  classroom: {
    id: string,
    name: string,
    students: []
  },
  userId: string,
  registryId: string,
  studentExams: [
    {
      studentId: string,
      examId: string,
      exam: {
        id: string,
        subjectId: string,
        subject: {
          id: string,
          name: string,
          exams: []
        },
        examDate: string,
        studentExams: []
      },
      grade: number
    }
  ]
}