﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparkle.Transfer.Data
{
    public class CompanyDto
    {
        public string? Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
