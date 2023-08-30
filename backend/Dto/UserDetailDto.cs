using backend.Models;
using backend.Utils;
using iText.StyledXmlParser.Jsoup.Select;

namespace backend.Dto;

public class UserDetailDto
{
    public Classroom? Classroom { get; set; }
    public UserDto User { get; set; }
    public RegistryDto Registry { get; set; }

}