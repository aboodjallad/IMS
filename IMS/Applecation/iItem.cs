using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Applecation
{
    internal interface iItem
    {
        bool AddItem(string name, int quantity, decimal price, string category);
        List<Good> GetItems();
        bool UpdateItem(int itemId, string name, int quantity, decimal price,int role);
        bool DeleteItem(int itemId, int role);
        string GetStatusBasedOnQuantity(int quantity);
        public void SearchByName(string searchName);
        public void SearchByCategory(string category);
        public void SearchByPriceASC();
        public void SearchByPriceDESC();

    }
}
