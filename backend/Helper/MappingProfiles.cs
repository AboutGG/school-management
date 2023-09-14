using AutoMapper;
using backend.Dto;
using backend.Models;

namespace backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<User, UserDto>();
        // CreateMap<UserDto, User>();
        CreateMap<RegistryDto, Registry>();
        CreateMap<Registry, RegistryDto>();
        CreateMap<Role, RoleDto>();
        

        CreateMap<User, UserList>();


        CreateMap<User, UserDetailDto>()
            .ForMember(dest => dest.Roles,
                opt => opt
                    .MapFrom(user => user.UsersRoles.Select(el => el.Role)))
            .ForMember(dest => dest.Classroom, 
                opt => opt
                    .MapFrom(el => el.Student.Classroom == null ? 
                                   el.TeachersSubjectsClassrooms
                                       .Select(r => r.Classroom) : null));

        // .ForMember(destinationMember => destinationMember.Roles,
        //     opt => opt
        //         .MapFrom(src => src.UsersRoles.Select(el => el.Role)));
        // .ForMember(destinationMember => destinationMember.Classrooms, 
        //     opt => opt
        //         .MapFrom(src => 
        //             src.Student.Classroom.Name));


        // CreateMap<User, UserDto>();
        //
        CreateMap<Classroom, ClassroomStudentCount>();
        CreateMap<Classroom, ClassroomDto>();
        CreateMap<ClassroomDto, Classroom>();

        // CreateMap<Teacher, TeacherDto>()
        //     .ForMember(destinationMember => destinationMember.id,
        //         opt => opt
        //             .MapFrom(src => src.UserId))
        //     .ForMember(destinationMember => destinationMember.name,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Name))
        //     .ForMember(destinationMember => destinationMember.surname,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Surname))
        //     .ForMember(destinationMember => destinationMember.subjects,
        //         opt =>
        //             opt.MapFrom(src => src.TeachersSubjectsClassrooms.Select(
        //                     tsc => tsc.Subject.Name).Distinct().ToList()));

        // CreateMap<Teacher, TeacherSubjectDto>()
        //     .ForMember(destinationMember => destinationMember.id,
        //         opt => opt
        //             .MapFrom(src => src.UserId))
        //     .ForMember(destinationMember => destinationMember.id,
        //         opt => opt
        //             .MapFrom(src => src.UserId))
        //     .ForMember(destinationMember => destinationMember.name,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Name))
        //     .ForMember(destinationMember => destinationMember.surname,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Surname))
        //     .ForMember(destinationMember => destinationMember.subjects,
        //         opt => opt
        //             .MapFrom(src => src.TeachersSubjectsClassrooms
        //                 .Select( el => new SubjectClassroomDto
        //                 {
        //                     Subject = new SubjectDto
        //                     {
        //                         Id = el.SubjectId,
        //                         Name = el.Subject.Name
        //                     },
        //                     Classroom = new ClassroomDto
        //                     {
        //                         Id = el.ClassroomId,
        //                         Name = el.Classroom.Name
        //                     }
        //                 })
        //             ));
        //
        // CreateMap<TeacherSubjectClassroom, TeacherSubjectClassroomDto>()
        //     .ForMember(destinationMember => destinationMember.teacher,
        //         opt => opt
        //             .MapFrom(src => src.Teacher));

        // CreateMap<Exam, TeacherExamDto>()
        //     .ForMember(destinationMember => destinationMember.ExamId,
        //         opt => opt
        //             .MapFrom(src => src.Id))
        //     .ForMember(destinationMember => destinationMember.ExamDate, 
        //         opt => opt
        //             .MapFrom(src => src.ExamDate))
        //     .ForMember(destinationMember => destinationMember.Classroom,
        //         opt => opt
        //             .MapFrom(src => src.TeacherSubjectClassroom.Classroom.Name))
        //     .ForMember(destinationMember => destinationMember.Subject,
        //         opt => opt
        //             .MapFrom(src => src.TeacherSubjectClassroom.Subject.Name));

        // CreateMap<Student, StudentDto>()
        //     .ForMember(destinationMember => destinationMember.id,
        //         opt => opt
        //             .MapFrom(src => src.UserId))
        //     .ForMember(destinationMember => destinationMember.name,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Name))
        //     .ForMember(destinationMember => destinationMember.surname,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Surname));
        //
        // CreateMap<Student, StudentExamDto>()
        //     .ForPath(dest => dest.Student.id,
        //         opt => opt
        //             .MapFrom(src => src.UserId))
        //     .ForPath(dest => dest.Student.name, 
        //         opt => opt
        //             .MapFrom(src => src.Registry.Name))
        //     .ForPath(dest => dest.Student.surname,
        //         opt => opt
        //             .MapFrom(src => src.Registry.Surname))
        //     .ForMember(destinationMember => destinationMember.Exams,
        //         opt => opt
        //             .MapFrom(src => src.StudentExams
        //                 .Select(el => new
        //                     {
        //                         subject = el.Exam.TeacherSubjectClassroom.Subject.Name,
        //                         date = el.Exam.ExamDate,
        //                         grade = el.Grade,
        //                         teacher = $"{el.Exam.TeacherSubjectClassroom.Teacher.Registry.Name} {el.Exam.TeacherSubjectClassroom.Teacher.Registry.Surname}"
        //                     }
        //                 )
        //             ));
        //
        // CreateMap<StudentExam, TeacherStudentExamDto>()
        //     .ForMember(destinationMember => destinationMember.Grade,
        //         opt => opt
        //             .MapFrom(src => src.Grade))
        //     .ForMember(destinationMember => destinationMember.Student,
        //         opt => opt
        //             .MapFrom(src => src.Student));

        // CreateMap<Exam, ExamDto>()
        //     .ForMember(destinationMember => destinationMember.StudentExams,
        //         opt => opt
        //             .MapFrom(src => src.StudentExams))
        //     .ForMember(destinationMember => destinationMember.ExamDate,
        //         opt => opt
        //             .MapFrom(src => src.ExamDate))
        //     .ForMember(destinationMember => destinationMember.Subject,
        //         opt => opt
        //             .MapFrom(src => src.TeacherSubjectClassroom.Subject.Name));


    }
}