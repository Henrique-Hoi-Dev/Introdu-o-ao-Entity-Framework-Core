using System;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // using var db = new Data.ApplicationContext();
            // db.Database.Migrate();
            // var existe = db.Database.GetPendingMigrations().Any();
            // if(existe)
            // {
            //     //
            // }

            // InserirDados();       
            // InserirDadosEmMassa();  
            ConsultarDados();   
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            // var ConsultaPorSintaxe = (from c in db.Cliente where c.Id>0 select c).ToList();
            var ConsultaPorMetodo = db.Cliente.Where(p=>p.Id >0).ToList();
            foreach(var cliente in ConsultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                db.Cliente.Find(cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123456789",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Henrique Hoinacki",
                CEP = "8578104",
                Cidade =  "Pato Branco",
                Estado = "PR",
                Telefone = "99995555"
            };

            var listaCliente = new[]
            {
                new Cliente
                {
                    Nome = "Pedro Hoinacki",
                    CEP = "8578124",
                    Cidade =  "Pato Branco",
                    Estado = "PR",
                    Telefone = "99229555"  
                },

                new Cliente
                {
                    Nome = "Joao Hoinacki",
                    CEP = "8578134",
                    Cidade =  "Pato Branco",
                    Estado = "PR",
                    Telefone = "99995255"  
                }
            };

            using var db = new Data.ApplicationContext();
            // db.AddRange(produto, cliente);
            db.Cliente.AddRange(listaCliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}"); 
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678910",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produto.Add(produto);
            db.Set<Produto>().Add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");

        }
    }
}
