using backend.Models;

namespace backend.Dto;

public class PromotionHistoryDto
{

    #region Costurctor

    public PromotionHistoryDto(PromotionHistory promotionHistory)
    {
        StudentId = promotionHistory.StudentId;
        PreviousClassroom = promotionHistory.PreviousClassroom;
        PreviousSchoolYear = promotionHistory.PreviousSchoolYear;
        PreviousClassroomId = promotionHistory.PreviousClassroomId;
        FinalGraduation = promotionHistory.FinalGraduation;
        ScholasticBehavior = promotionHistory.ScholasticBehavior;
        Promoted = promotionHistory.Promoted;
    }

    #endregion

    #region Attributes

    public Guid StudentId { get; set; }

    public Guid PreviousClassroomId { get; set; }
    public virtual Classroom PreviousClassroom { get; set; }

    public string PreviousSchoolYear { get; set; }

    public int FinalGraduation { get; set; }

    public int ScholasticBehavior { get; set; }

    public bool Promoted { get; set; }

    #endregion


}