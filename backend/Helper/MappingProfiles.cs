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
        CreateMap<Registry, RegistryDto>();
        CreateMap<RegistryDto, Registry>();
        CreateMap<UserDetailDto, Student>();
        CreateMap<UserDetailDto, Teacher>();
        CreateMap<Student, UserDetailDto>();
        CreateMap<Teacher, UserDetailDto>();
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
            .ForMember(dest => dest.id_classroom,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.name_classroom,
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
                    opt.MapFrom(src =>
                        src.TeacherSubjectsClassrooms.Select(
                            tsc => new { subject = tsc.Subject.Name, Classroom = tsc.Classroom.Name }).ToList()));

        CreateMap<TeacherSubjectClassroom, TeacherSubjectClassroomDto>()
            .ForMember(destinationMember => destinationMember.teacher,
                opt => opt
                    .MapFrom(src => src.Teacher));

        CreateMap<Student, StudentDto>()
            .ForMember(destinationMember => destinationMember.Id,
                opt => opt
                    .MapFrom(src => src.UserId))
            .ForMember(destinationMember => destinationMember.Name,
                opt => opt
                    .MapFrom(src => src.Registry.Name))
            .ForMember(destinationMember => destinationMember.Surname,
                opt => opt
                    .MapFrom(src => src.Registry.Surname))
            .ForMember(destinationMember => destinationMember.Classroom,
                opt => opt
                    .MapFrom(src => src.Classroom.Name));

        CreateMap<Student, StudentExamDto>()
            .ForMember(destinationMember => destinationMember.Student,
                opt => opt
                    .MapFrom(src =>
                        new StudentDto
                        {
                            Id = src.UserId,
                            Name = src.Registry.Name,
                            Surname = src.Registry.Surname
                        }))
            .ForMember(destinationMember => destinationMember.Exams,
                opt => opt
                    .MapFrom(src => src.StudentExams
                        .Select(el => new
                            {
                                subject = el.Exam.Subject.Name,
                                date = el.Exam.ExamDate,
                                grade = el.Grade,
                            }
                        )
                    ));
    }
}