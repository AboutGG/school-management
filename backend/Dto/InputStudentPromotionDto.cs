namespace backend.Dto;

public class InputStudentPromotionDto
{
    public int ScholasticBehavior { get; set; }
    public bool Promoted { get; set; }
    
    public Guid NextClassroom { get; set; }
}