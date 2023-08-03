﻿using backend.Utils;

namespace backend.DTO;

public class UserDTO
{
    /// <summary> User username </summary>
    [StringValidator(3, ErrorMessage = "Username cannot contain less then 3 character")]
    public string Username { get; set; }

    /// <summary> User password </summary>
    [StringValidator(3, ErrorMessage = "Password cannot contain less then 3 character")]
    public string Password { get; set; }
}

