using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repository.DTOs.Group
{
    public class GroupDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
      //  public Education  Education { get; set; }
    }
}
