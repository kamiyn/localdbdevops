using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LocalDbDevops.Core.Entities;
using LocalDbDevops.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocalDbDevops.Core
{
    public class DevopsRepository : IDevopsRepository, IDisposable
    {
        private readonly DevopsDbContext db;

        public DevopsRepository(DevopsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<IProduct>> GetAllProducts(CancellationToken ct)
        {
            const string sql = @"
SELECT [Id]
      ,[Name]
      ,[Price]
      ,[Stock]
      ,[TimeStamp]
  FROM [dbo].[Product]";
            var conn = db.Database.GetDbConnection();
            return await conn.QueryAsync<Product>(new CommandDefinition(sql, cancellationToken: ct));
        }

        public Task Clear(CancellationToken ct)
        {
            var conn = db.Database.GetDbConnection();
            return conn.ExecuteAsync(new CommandDefinition("DELETE FROM [dbo].[Product];", cancellationToken: ct));
        }

        public Task AddProducts(IEnumerable<IProduct> p, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO [dbo].[Product] (
       [Id]
      ,[Name]
      ,[Price]
      ,[Stock]
      -- ,[TimeStamp]
) VALUES (
       @Id
      ,@Name
      ,@Price
      ,@Stock
      -- ,[TimeStamp]
)";
            var conn = db.Database.GetDbConnection();
            return conn.ExecuteAsync(new CommandDefinition(sql, p, cancellationToken: ct));
        }

        public Task<int> DeleteProductsById(IEnumerable<long> ids, CancellationToken ct)
        {
            const string sql = @"
DELETE FROM [dbo].[Product] WHERE Id IN @ids";
            var conn = db.Database.GetDbConnection();
            return conn.ExecuteAsync(new CommandDefinition(sql, new {ids = ids}, cancellationToken: ct));
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}
