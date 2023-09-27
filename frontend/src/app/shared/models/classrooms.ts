import { Teachers, Students } from "./users";
export interface Classroom {
    id_classroom?: string;
    name_classroom: string;
    student_count: number;
  }

  export interface ClassDetails {
    teachers: Teachers[];
    students: Students[];
  }