﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.ApplicationLayer.Dtos.AuthDtos
{
    public class AuthResponseDto
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsAdmin { get; set; }
    }
}
