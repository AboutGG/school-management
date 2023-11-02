export interface StudentGraduation {
  scholasticBehavior: number;
  promoted: boolean;
  nextClassroom: string;
}

export interface Grade {
  fullName: string;
  finalGrade: number;
}

export interface HistoryGraduation {
  studentId: string;
  previousClassroomId: string;
  previousSchoolYear: string;
  finalGraduation: number;
  scholasticBehavior: number;
  promoted: boolean;
}