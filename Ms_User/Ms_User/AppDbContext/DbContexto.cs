using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ms_User.Models;

namespace Ms_User.AppDbContext
{
public class DbContexto : IdentityDbContext<ApplicationUser>
{
    public DbContexto(DbContextOptions<DbContexto> options)
        : base(options)
    {}
}
}