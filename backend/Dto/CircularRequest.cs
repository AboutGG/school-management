using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace backend.Dto;

public class CircularRequest
{
    public int number { get; set; }
    
    public DateOnly date { get; set; }
    
    public string location { get; set; }

    public string header { get; set; }

    public string body { get; set; }

    public string sign { get; set; }
}