using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Api.NetCore.Domains
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}