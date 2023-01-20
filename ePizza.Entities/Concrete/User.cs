using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ePizza.Entities.Concrete
{
    public class User: IdentityUser<int>
    {
        public string Name { get; set; }



        [NotMapped]
        public string[] Roles { get; set; }
    }
}
