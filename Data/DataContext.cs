using Microsoft.EntityFrameworkCore;
using ArmstrongServer.Models;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Data
{
  public class DataContext : DbContext
  {
    // public DbSet<Channel>? Channels { get; set; }
    // public DbSet<History>? Histories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      // var config = SettingsHelper.GetConfiguration();
      // var sqlConfig = config.GetSection("SqlConnectionSettings")
      //                       .Get<SqlConnectionSettings>();
      var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder
      {
        Host = "localhost",
        Port = 5432,
        Username = "postgres",
        Password = "postgres",
        Database = "arms_webapp_development"
      };

      Console.WriteLine(connectionStringBuilder.ConnectionString.ToString());

      options.UseNpgsql(connectionStringBuilder.ConnectionString);
    }
  }
}
