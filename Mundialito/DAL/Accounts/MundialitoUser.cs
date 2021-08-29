using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mundialito.DAL.Accounts
{
    public class MundialitoUser : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }
    }
}