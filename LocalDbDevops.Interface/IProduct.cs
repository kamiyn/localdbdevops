using System;

namespace LocalDbDevops.Interface
{
    public interface IProduct
    {
        long Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Stock { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
