using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{ 
    public enum IngredientType
    {
        Shot, Water, Milk, Ice, Chocolate, Strawberry
    }
    public class Ingredient
    {
        public IngredientType Type { get; set; }
        public int Price {  get; set; }

        public Queue<ConsoleKey> Command {  get; set; }

        public virtual void PutIngredient()
        {

        }
    }

    public class PlusIngredient : Ingredient
    {
        public override void PutIngredient()
        {
            
        }
    }
}
