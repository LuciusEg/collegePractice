using System;
using System.Collections.Generic;

namespace documentation.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? Role { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }
}
