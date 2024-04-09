using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Services;

namespace IMS.Applecation
{
    public class Good
    {
        public string category { get; set; }
        public int GoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            ItemService item =new ItemService("Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123");
            string status = item.GetStatusBasedOnQuantity(this.Quantity);
            return $" Name: {Name}, Quantity: {Quantity}, Price: {Price} , Status : {status}";
        }
    }

}
