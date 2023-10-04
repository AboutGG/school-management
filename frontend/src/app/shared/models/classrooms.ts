import { Teachers, Students } from "./users";
export interface TeacherClassroom {
    id_classroom?: string;
    name_classroom: string;
    student_count: number;
  }

  export interface ClassDetails {
    teachers: Teachers[];
    students: Students[];
    name_classroom: string;
  }

  export interface Classroom {
    id: string;
    name:string;

  }

