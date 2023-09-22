using backend.Models;

namespace backend.Dto;

public class Me
{
    //public Guid UserId { get; set; }
    public String Name { get; set; }
    public Guid RegistryId { get; set; }
    public Guid? ClassroomId { get; set; }

    public Me(User user)
    {
        if (user.Student != null)
            this.Name = user.Student.Registry.Name + " " + user.Student.Registry.Surname;
        else
            this.Name = user.Teacher.Registry.Name + " " + user.Teacher.Registry.Surname;
        
       // this.Name =string.Concat( user.Student?.Registry.Name + " " + user.Student?.Registry.Surname) ?? user.Teacher.Registry.Name  : ;
       // this.UserId = user.Id;
        this.RegistryId = user.Student?.RegistryId ?? user.Teacher?.RegistryId ?? Guid.Empty;
        this.ClassroomId = user.Student?.ClassroomId;
    }


}