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

export interface UsersMe{
  id: string;
  name: string;
  classroomId: string;
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

export interface Student {
  id: string;
  registryId: string;
  userId: string;
  classroom: string;
}

export interface Students {
  id: string;
  name: string;
  surname: string;
  fullName: string;
  finalGrade: number;
}

export interface Teacher {
  id: string;
  registryId: string;
  userId: string;
}

export interface Teachers {
  id?: string;
  name: string;
  surname: string;
  subjects: string[];
}