﻿using backend.UserRoles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities.User
{
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [EnumDataType(typeof(EUserRole))]
        public EUserRole? Role { get; set; }
        public int? Balance { get; set; }
    }
}
