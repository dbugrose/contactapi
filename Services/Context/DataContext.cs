using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contactapi.Models;
using Microsoft.EntityFrameworkCore;

namespace contactapi.Services.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
                public DbSet<ContactModel> Contacts {get; set;}
    }
}