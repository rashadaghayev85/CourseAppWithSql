using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Education
{
    public class EducationWithGroupsDto
    {
        public string Education { get; set; }
        public List<string> Groups { get; set; }
    }
}
