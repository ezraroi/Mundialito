using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{
    public class MundialitoUser : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}