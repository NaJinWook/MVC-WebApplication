using AspnetCoreWeb.MVC6.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWeb.MVC6.DataContext
{
    public class DatabaseContext : DbContext // DbContext는 EntityFramework에 있는 DbContext를 상속받음
    {
        public DbSet<User> Users { get; set; } // 마이그레이션으로 넘어가면 데이터베이스가 자동 생성된다.

        public DbSet<Note> Notes { get; set; } // 마이그레이션으로 넘어가면 데이터베이스가 자동 생성된다.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // ConnectionString 용도
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\ProjectsV13;uid=root;pwd=1234;database=NoteDb;");
            // @는 있는 그대로 보내겠다라는 의미
        }
    }
}

