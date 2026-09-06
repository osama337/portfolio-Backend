using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace portfolio.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string? ResumeUrl { get; set; }

        // Navigation Properties
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    }
}
