using System;
using System.Collections.Generic;

namespace CrudExample.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createdon { get; set; }

    public DateTime? Updatedon { get; set; }

    public DateTime? Deletedon { get; set; }

    public int? DeptId { get; set; }
}
