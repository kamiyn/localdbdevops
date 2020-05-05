using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocalDbDevops.Interface
{
    public interface IDevopsRepository
    {
        Task<IEnumerable<IProduct>> GetAllProducts(CancellationToken ct);
        Task Clear(CancellationToken ct);
        Task AddProducts(IEnumerable<IProduct> p, CancellationToken ct);
        Task<int> DeleteProductsById(IEnumerable<long> ids, CancellationToken ct);
    }
}
