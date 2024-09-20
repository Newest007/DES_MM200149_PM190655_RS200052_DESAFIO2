using AplicacionAPI_Desafio2_MM200149_PM190655.Controllers;
using AplicacionAPI_Desafio2_MM200149_PM190655.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrganizadoresAPI.Tests
{
    public class OrganizadoresControllerTests
    {
        [Fact]
        public async Task PostOrganizador_AgregandoOrganizador_ConLosCamposValidos()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new OrganizadoresController(context);
            var nuevoOrganizador = new Organizador { Nombre = "Nombre de Organizador", Cargo = "Responsable de Proyecto", EventoId = 1 };

            var result = await controller.PostOrganizador(nuevoOrganizador);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var organizador = Assert.IsType<Organizador>(createdResult.Value);
            Assert.Equal("Nombre de Organizador", organizador.Nombre);
        }

        [Fact]
        public async Task GetOrganizador_RetornaOrganizador_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new OrganizadoresController(context);
            var organizador = new Organizador { Nombre = "Nombre de Prueba", Cargo = "Jefe de proyecto", EventoId = 1  };
            context.Organizadores.Add(organizador);
            await context.SaveChangesAsync();

            var result = await controller.GetOrganizador(organizador.OrganizadorId);

            var actionResult = Assert.IsType<ActionResult<Organizador>>(result);
            var returnValue = Assert.IsType<Organizador>(actionResult.Value);
            Assert.Equal("Nombre de Prueba", returnValue.Nombre);
        }

        [Fact]
        public async Task GetOrganizador_RetornaNotFound_CuandoIdNoExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new OrganizadoresController(context);

            var result = await controller.GetOrganizador(999);

            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task PostOrganizador_IncrementaConteo_CuandoSeAgregaNuevoOrganizador()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new OrganizadoresController(context);
            var organizadorInicial = new Organizador { Nombre = "Organizador 1", Cargo = "Cargo 1", EventoId = 1 };

            await controller.PostOrganizador(organizadorInicial);

            var nuevoOrganizador = new Organizador { Nombre = "Organizador 2", Cargo = "Cargo 2", EventoId=1 };

            await controller.PostOrganizador(nuevoOrganizador);
            var organizadores = await context.Organizadores.ToListAsync();

            Assert.Equal(2, organizadores.Count);

        }
    }
}
