﻿using System;
using System.Collections.Generic;

namespace WebAppSchoolApiClientDemo.Models;

public class Course
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
