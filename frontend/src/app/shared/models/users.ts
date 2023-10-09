import { TeacherSubject } from "./subjects"

export interface Users {
  classroom?: string
  classroomId?: string
  user: User
  registry: Registry
  role: string
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
  id?: string;
  name: string;
  surname: string;
  gender: string;
  birth?: string;
  email?: string;
  address?: string;
  telephone?: string; //date format "yyyy-mm-dd"
  role?: string;

  teacher?: Teacher;
  student?: Student;
}

export interface ListResponse<T> {
  data: T,
  total: number
}

export interface Teacher {
  id: string;
  registryId: string;
  userId: string;
}

export interface Student {
  id: string;
  registryId: string;
  userId: string;
  classroom: string;
}

export interface StudentExams {
  student: {
    id: string,
    name: string,
    surname: string
  },
  exams: [
    {
      subject: string,
      teacher: string,
      date: string,
      grade?: number
    }
  ]
}

// export interface TeacherExam {
//   examId: string,
//   examDate: string,
//   classroom: string,
//   subject: string
// }

// export interface Classroom {
//   id_classroom?: string;
//   name_classroom: string;
//   student_count: number;
// }

export interface Teacher {
  id: string,
  name: string,
  surname: string,
  subjects: TeacherSubject[]
}

// export interface ListResponse {
//   data: any[],
//   total: number
// }

// export interface TeacherSubject {
//   subjectId: string
//   subjectName: string,
//   classroomId: string
//   classroomName: string
// }

export interface Teachers {
  id?: string;
  name: string;
  surname: string;
  subjects: string[];
}

export interface Students {
  id?: string;
  name: string;
  surname: string;
}

export interface UsersMe{
  id: string;
  name: string;
  classroomId: string;
}



