using backend.Models;

namespace backend.Dto;

public class Me
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public Guid? ClassroomId { get; set; }

    public Me(User user)
    {
        if (user.Student != null)
            this.Name = user.Student.Registry.Name + " " + user.Student.Registry.Surname;
        else
            this.Name = user.Teacher.Registry.Name + " " + user.Teacher.Registry.Surname;
        
        this.Id = user.Id;
        this.ClassroomId = user.Student?.ClassroomId;
    }


}