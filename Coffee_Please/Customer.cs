using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public class Customer
    {
        public Drink DrinkToOrder { get; set; }
        public List <PlusIngredient> Requires {  get; set; }
        public List<Drink> OrderList { get; set; }
        
        public void Order()
        {

        }

        public void CheckMenu()
        {

        }

        public void GiveMoney()
        {

        }

    }
}
