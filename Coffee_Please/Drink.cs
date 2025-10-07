using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public class DrinkFactory
    {
        public enum DrinkType
        {
            Espresso, Americano, CaffeLatte, ChocolateLatte, StrawberryLatte
        }
        public class Drink
        {
            public DrinkType type { get; set; }
            public List<IngredientFactory.Ingredient> Recipe { get; set; }
            public int Price { get; set; }
        }
        public void MakeFirstDayDrinks()//첫날 해금된 메뉴 주문가능목록에 넣기.
        {
            for (int i = 0; i < Enum.GetValues(typeof(DrinkType)).Length; i++)
            {
                Customer.Orderables.Add(MakeDrink((DrinkType)i));                
            }
        }

        public Drink MakeDrink(DrinkType type)
        {
            Drink drink = new Drink();
            drink.Recipe = SetRecipe(type);
            drink.Price = SetPrice(drink);
            return drink;
        }

        public List<IngredientFactory.Ingredient> SetRecipe(DrinkType type) //음료 레시피에 재료 할당하는 매서드(음료 레시피 설정 및 수정은 여기로!).
        {
            List<IngredientFactory.Ingredient> recipe = new List<IngredientFactory.Ingredient>();

            switch (type)
            {
                case DrinkType.Espresso:
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                    break;
                case DrinkType.Americano:
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Water]);
                    break;
                case DrinkType.CaffeLatte:
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                    break;
                case DrinkType.ChocolateLatte:
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Chocolate]);
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);                    
                    break;
                case DrinkType.StrawberryLatte:
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Strawberry]);
                    recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                    break;
            }
            return recipe;
        }

        public int SetPrice(Drink drink)//음료에 들어간 재료값의 합 * 마진율을 음료값으로 할당하는 매서드(음료 마진율 수정은 여기로!).
        {
            int price = 0;
            for (int i = 0; i < drink.Recipe.Count; i++)
            {
                price += drink.Recipe[i].Price;
            }
            price = price * 130 / 100; //현재 마진율은 약 [1.3]입니다(소수점 버림).
            return price;
        }
    }
}
