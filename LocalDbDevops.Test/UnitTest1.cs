using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalDbDevops.Core;
using LocalDbDevops.Core.Entities;
using LocalDbDevops.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDbDevops.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var cts = new CancellationTokenSource();

            var optionsBuilder = new DbContextOptionsBuilder<DevopsDbContext>();
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LocalDbDevops;Integrated Security=True;Connect Timeout=30;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                ;
            var db = new DevopsDbContext(optionsBuilder.Options);

            IDevopsRepository repo = new DevopsRepository(db);
            await repo.Clear(cts.Token);
            {
                var products = await repo.GetAllProducts(cts.Token);
                Assert.IsFalse(products.Any());
            }
            await repo.AddProducts(GetSampleProducts(), cts.Token);
            {
                var products = (await repo.GetAllProducts(cts.Token)).ToArray();
                Assert.IsTrue(products.Any());
                Assert.AreEqual(2, products.Count());
            }
            {
                var result = await repo.DeleteProductsById(new long[] {12345}, cts.Token);
                Assert.AreEqual(1, result);
            }
            {
                var products = (await repo.GetAllProducts(cts.Token)).ToArray();
                Assert.IsTrue(products.Any());
                Assert.AreEqual(1, products.Count());
                Assert.AreEqual(12346, products[0].Id);
                Assert.AreEqual(34567M, products[0].Price);
                Assert.AreEqual("🍎", products[0].Name);
                Assert.AreEqual(4, products[0].Stock);
            }
        }

        private static IEnumerable<IProduct> GetSampleProducts()
        {
            yield return new Product{
                Id=12345,
                Price= 23456,
                Name = "🍅",
                Stock = 3,
            };
            yield return new Product
            {
                Id = 12346,
                Price = 34567,
                Name = "🍎",
                Stock = 4,
            };
        }
    }
}
