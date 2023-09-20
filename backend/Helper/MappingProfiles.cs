using AutoMapper;
using backend.Dto;
using backend.Models;

namespace backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<RegistryDto, Registry>();
        CreateMap<Registry, RegistryDto>();
        
        
        
        // CreateMap<User, UserDetailDto>()
        //     .ForMember(dest => dest.Registry,
        //         opt => opt
        //             .MapFrom(src => src.Teacher != null ? src.Teacher.Registry : src.Student.Registry))
        //     .ForPath(dest => dest.User.Id,
        //         opt => opt
        //             .MapFrom(user => user.Id))
        //     .ForPath(dest => dest.User.Username,
        //         opt => opt
        //             .MapFrom(user => user.Username))
        //     .ForPath(dest => dest.User.Password,
        //         opt => opt
        //             .MapFrom(user => user.Password));
        
        CreateMap<Teacher, UserDto>();

        CreateMap<Classroom, ClassroomStudentCount>()
            .ForMember(dest => dest.id_classroom,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.name_classroom,
                opt => opt
                    .MapFrom(src => src.Name))
            .ForMember(dest => dest.student_count,
                opt => opt
                    .MapFrom(src => src.Students.Count()));

        CreateMap<Classroom, ClassroomDto>()
            .ForMember(dest => dest.Id,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.Name,
                opt => opt
                    .MapFrom(src => src.Name));

        CreateMap<Teacher, TeacherDto>()
            .ForMember(destinationMember => destinationMember.id,
                opt => opt
                    .MapFrom(src => src.UserId))
            .ForMember(destinationMember => destinationMember.name,
                opt => opt
                    .MapFrom(src => src.Registry.Name))
            .ForMember(destinationMember => destinationMember.surname,
                opt => opt
                    .MapFrom(src => src.Registry.Surname))
            .ForMember(destinationMember => destinationMember.subjects,
                opt =>
                    opt.MapFrom(src => src.TeachersSubjectsClassrooms.Select(
                            tsc => tsc.Subject.Name).Distinct().ToList()));

        CreateMap<Teacher, TeacherSubjectDto>()
            .ForMember(destinationMember => destinationMember.id,
                opt => opt
                    .MapFrom(src => src.UserId))
            .ForMember(destinationMember => destinationMember.name,
                opt => opt
                    .MapFrom(src => src.Registry.Name))
            .ForMember(destinationMember => destinationMember.surname,
                opt => opt
                    .MapFrom(src => src.Registry.Surname))
            .ForMember(destinationMember => destinationMember.subjects,
                opt => opt
                    .MapFrom(src => src.TeachersSubjectsClassrooms
                        .Select( el => new SubjectClassroomDto
                        {
                            SubjectId = el.SubjectId,
                            ClassroomId = el.ClassroomId,
                            SubjectName = el.Subject.Name,
                            ClassroomName = el.Classroom.Name
                        })
                    ));

        CreateMap<TeacherSubjectClassroom, SubjectClassroomDto>()
            .ForMember(destinationMember => destinationMember.ClassroomId,
                opt => opt
                    .MapFrom(src => src.ClassroomId))
            .ForMember(destinationMember => destinationMember.ClassroomName,
                opt => opt
                    .MapFrom(src => src.Classroom.Name))
            .ForMember(destinationMember => destinationMember.SubjectId,
                opt => opt
                    .MapFrom(src => src.SubjectId))
            .ForMember(destinationMember => destinationMember.SubjectName,
                opt => opt
                    .MapFrom(src => src.Subject.Name));
        

        CreateMap<Exam, TeacherExamDto>()
            .ForMember(destinationMember => destinationMember.ExamId,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(destinationMember => destinationMember.ExamDate, 
                opt => opt
                    .MapFrom(src => src.ExamDate))
            .ForMember(destinationMember => destinationMember.Classroom,
                opt => opt
                    .MapFrom(src => src.TeacherSubjectClassroom.Classroom.Name))
            .ForMember(destinationMember => destinationMember.Subject,
                opt => opt
                    .MapFrom(src => src.TeacherSubjectClassroom.Subject.Name));
        
        CreateMap<Student, StudentDto>()
            .ForMember(destinationMember => destinationMember.id,
                opt => opt
                    .MapFrom(src => src.UserId))
            .ForMember(destinationMember => destinationMember.name,
                opt => opt
                    .MapFrom(src => src.Registry.Name))
            .ForMember(destinationMember => destinationMember.surname,
                opt => opt
                    .MapFrom(src => src.Registry.Surname));

        CreateMap<Student, StudentExamDto>()
            .ForPath(dest => dest.Student.id,
                opt => opt
                    .MapFrom(src => src.UserId))
            .ForPath(dest => dest.Student.name, 
                opt => opt
                    .MapFrom(src => src.Registry.Name))
            .ForPath(dest => dest.Student.surname,
                opt => opt
                    .MapFrom(src => src.Registry.Surname))
            .ForMember(destinationMember => destinationMember.Exams,
                opt => opt
                    .MapFrom(src => src.StudentExams
                        .Select(el => new
                            {
                                subject = el.Exam.TeacherSubjectClassroom.Subject.Name,
                                date = el.Exam.ExamDate,
                                grade = el.Grade,
                                teacher = $"{el.Exam.TeacherSubjectClassroom.Teacher.Registry.Name} {el.Exam.TeacherSubjectClassroom.Teacher.Registry.Surname}"
                            }
                        )
                    ));

        CreateMap<StudentExam, TeacherStudentExamDto>()
            .ForMember(destinationMember => destinationMember.Grade,
                opt => opt
                    .MapFrom(src => src.Grade))
            .ForMember(destinationMember => destinationMember.Student,
                opt => opt
                    .MapFrom(src => src.Student));
        
        CreateMap<Exam, ExamDto>()
            .ForMember(destinationMember => destinationMember.StudentExams,
                opt => opt
                    .MapFrom(src => src.StudentExams))
            .ForMember(destinationMember => destinationMember.ExamDate,
                opt => opt
                    .MapFrom(src => src.ExamDate))
            .ForMember(destinationMember => destinationMember.Subject,
                opt => opt
                    .MapFrom(src => src.TeacherSubjectClassroom.Subject.Name));
        
        
    }
}