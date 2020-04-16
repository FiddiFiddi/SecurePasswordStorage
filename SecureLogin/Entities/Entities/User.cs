using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    [Table("Users")]
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
