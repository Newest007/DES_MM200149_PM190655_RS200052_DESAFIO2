using AplicacionAPI_Desafio2_MM200149_PM190655.Models;
using Microsoft.EntityFrameworkCore;

namespace OrganizadoresAPI.Tests
{
    public static class Setup
    {
        public static ConexionContext GetInMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ConexionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ConexionContext(options);
            context.Database.EnsureCreated();
            return context;
        }


    }
}
