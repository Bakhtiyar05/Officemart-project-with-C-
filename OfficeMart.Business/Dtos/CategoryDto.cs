﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Sahə tələb olunandır")]
        [MinLength(2,ErrorMessage ="Minimum uzunluq 2 hərfdən ibarət olmalıdır")]
        public string CategoryName { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
