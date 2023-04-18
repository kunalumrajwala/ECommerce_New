using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal price { get; set; }

        public string PictureUrl { get; set; }

        public string productType { get; set; }

        public int ProductTypeId { get; set; }

        public string productBrand { get; set; }

        public int ProductBrandId { get; set; }
    }
}