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

export interface Classroom {
  id_classroom?: string;
  name_classroom: string;
}

export interface Teachers {
  id?: string;
  name: string;
  surname: string;
  subject: string[];
}

export interface Students {
  id?: string;
  name: string;
  surname: string;
  gender: string;
}
export interface ClassDetails {
  teachers: Teachers[];
  students: Students[];
}

