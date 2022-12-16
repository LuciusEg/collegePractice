using System;
using System.Collections.Generic;

namespace documentation.Models;

public partial class Document
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Number { get; set; }

    public int? Author { get; set; }

    public int? Recipient { get; set; }
}
