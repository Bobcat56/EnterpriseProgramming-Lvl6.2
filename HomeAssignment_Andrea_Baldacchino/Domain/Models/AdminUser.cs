﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AdminUser: IdentityUser
    {
        public Boolean IsAdmin { get; set; }
    }
}