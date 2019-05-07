using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NLayerApp.WEB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Worker { get; set; }
        public ApplicationUser()
        {
        }
    }
}