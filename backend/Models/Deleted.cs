using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

public class Deleted
{
    [Column("deleted_at")]
    [JsonPropertyName("deleted_at")]
    public DateOnly? DeletedAt { get; set; }

    [Column("isDeleted")]
    [JsonPropertyName("isDeleted")]
    public bool isDeleted { get; set; } = false;
}