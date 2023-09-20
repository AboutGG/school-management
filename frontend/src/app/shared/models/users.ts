export interface Users {
  classroom?: string
  user: User
  registry: Registry
}

export interface Prova {
  classroomId?: string
  user: User 
  registry: Registry
  roleName: string
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
  roleName?: string;

  teacher?: Teacher;
  student?: Student;
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
export interface Classroom {
  id: string;
  id_classroom?: string;
  name: string;
  student_count?: number;
}

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
export interface ClassDetails {
  teachers: Teachers[];
  students: Students[];
}
