using FusionAPI_Framework.Models;
using System;
using System.Collections.Generic;

namespace FusionAPI_Framework.Models;

public partial class Labour
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Department { get; set; }

    public bool? Isactive { get; set; }
  //  public virtual ICollection<Products>? Products { get; set; }
}