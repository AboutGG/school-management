using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("Pdfs")]
public class Circular : Deleted
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("circular_number")]
    [JsonPropertyName("circular_name")]
    public int CircularNumber { get; set; }
    
    [Column("upload_date")]
    [JsonPropertyName("upload_date")]
    public DateOnly UploadDate { get; set; }
    
    [Column("location")]
    [JsonPropertyName("location")]
    public string Location { get; set; }

    [Column("header")]
    [JsonPropertyName("header")]
    public string Header { get; set; }
    
    [Column("object")] 
    [JsonPropertyName("object")]
    public string Object { get; set; }
    
    [Column("body")]
    [JsonPropertyName("body")]
    public string Body { get; set; }

    [Column("sign")]
    [JsonPropertyName("sign")]
    public string Sign { get; set; }
}