using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CovidCrossingServer
{
    public class LeaderboardContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=leaderboard.db");
    }

    public class Record
    {
        [Key]
        public Guid Guid { get; set; }
        public string Nickname { get; set; }
        public int HighScore { get; set; }
    }
}
