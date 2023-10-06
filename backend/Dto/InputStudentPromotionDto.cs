namespace backend.Dto;

public class InputStudentPromotionDto
{
    public string SchoolYear { get; set; }
    public int ScholasticBehavior { get; set; }
    public bool Promoted { get; set; }
    
    public Guid NextClassroom { get; set; }
}