using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Group:BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int EducationId { get; set; }
        [Required]
        public Education Education { get; set; }
    }
}
