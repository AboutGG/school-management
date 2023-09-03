using System.Collections;
using backend.Models;

namespace backend.Interfaces;

public interface IClassroomRepository
{
    int GetClassroomsCount();
    List<Classroom> GetClassrooms();

}