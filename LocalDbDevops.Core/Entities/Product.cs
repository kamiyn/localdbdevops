using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LocalDbDevops.Interface;

namespace LocalDbDevops.Core.Entities
{
    public partial class Product : IProduct
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
