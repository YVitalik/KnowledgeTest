﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.EditTestDTOs
{
    public class CreateQuestionDto
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answear { get; set; }
    }
}
