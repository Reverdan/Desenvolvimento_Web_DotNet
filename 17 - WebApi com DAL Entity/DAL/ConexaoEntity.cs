

using CRUDPessoas.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CRUDPessoas.DAL;

// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet tool install --global dotnet-ef
// winget install Microsoft.DotNet.SDK.10

public class ConexaoEntity : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0BMMDJG\SQLEXPRESS;
        Initial Catalog=ds34a;User ID=sa;Password=unip;Encrypt=False");
    }
}