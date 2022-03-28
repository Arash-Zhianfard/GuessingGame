﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SigninRequest
    {
        [Required(ErrorMessage = "Wrong UserName")]
        [MaxLength(15, ErrorMessage = "Max Error")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(30, ErrorMessage = "Max Error")]
        public string Password { get; set; }
    }
}