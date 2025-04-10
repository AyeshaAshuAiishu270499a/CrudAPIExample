using System;
using System.Collections.Generic;

namespace CrudExample.Entities;

public partial class Department
{
    public int DeptId { get; set; }

    public string? Name { get; set; }

    public DateTime? Createdon { get; set; }

    public DateTime? Updatedon { get; set; }

    public DateTime? Deletedon { get; set; }
}
