using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Education:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]  
        public string Color { get; set; }
        [AllowNull]
        public List<Group> Group { get; set; }
    }
}
