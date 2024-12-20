using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC_App.Models;

public partial class AspNetRole
{
    public AspNetRole()
    {
        AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
        UserRoles = new HashSet<AspNetUserRole>();
    }

    [Key]
    public string Id { get; set; } = null!;

    [StringLength(256)]
    public string? Name { get; set; }

    [StringLength(256)]
    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    // Navigation property for AspNetUserRole
    [InverseProperty("Role")]
    public virtual ICollection<AspNetUserRole> UserRoles { get; set; }
}
