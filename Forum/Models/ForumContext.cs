﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;

namespace Forum.Models
{
    public class ForumContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TopicLog> TopicLogs { get; set; }
        public DbSet<LogonLog> LogonLogs { get; set; }

        public ForumContext (DbContextOptions <ForumContext> options) : base(options) { }

        public DbSet<Forum.Models.Logon> Logon { get; set; }
    }
}
