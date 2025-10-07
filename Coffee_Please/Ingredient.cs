using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Coffee_Please
{
    public enum IngredientType //재료 타입(재료 종류 추가 및 수정은 여기로!).
    {
        Shot, Water, Milk, Ice, Chocolate, Strawberry
    }


    public class IngredientFactory //재료 객체 생성을 전담할 팩토리 클래스.
    {
        public static Dictionary<IngredientType, Ingredient> Ingredients { get; set; } 
        public static List<PlusIngredient> PlusIngredients { get; set; }

        public class Ingredient //재료 클래스.
        {
            public IngredientType Type { get; set; }
            public int Price { get; set; }

            public Queue<ConsoleKey> Command { get; set; }

            public virtual void PutIngredient() //만드는 음료에 재료를 추가하는 매서드.
            {

            }
        }

        public class PlusIngredient : Ingredient //손님의 추가 요구사항에 해당하는 추가 재료 클래스.
        {
            public override void PutIngredient()
            {

            }
        }

        public void MakeAllIngredients()
        {
            for (int i = 0; i < Enum.GetValues(typeof(IngredientType)).Length; i++)
            {
                Ingredients.Add((IngredientType)i, MakeIngredient((IngredientType)i));
            }
        }


        private Ingredient MakeIngredient(IngredientType type) //재료 타입 입력하면 가격과 커맨드 할당해주는 매서드.
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Price = SetPrice(type);
            ingredient.Command = SetCommand(type);            
            return ingredient;
        }

        private int SetPrice(IngredientType type)//재료에 가격 할당하는 매서드(재료 가격 설정 및 수정은 여기로!)
        {
            Ingredient ingredient = new Ingredient();

            switch (type)
            {
                case IngredientType.Shot:
                    ingredient.Price = 2;
                    break;
                case IngredientType.Water:
                    ingredient.Price = 1;
                    break;
                case IngredientType.Milk:
                    ingredient.Price = 2;
                    break;
                case IngredientType.Ice:
                    ingredient.Price = 1;
                    break;
                case IngredientType.Chocolate:
                    ingredient.Price = 3;
                    break;
                case IngredientType.Strawberry:
                    ingredient.Price = 3;
                    break;
            }

            return ingredient.Price;
        }
        private Queue<ConsoleKey> SetCommand(IngredientType type) //재료에 키 커맨드 할당하는 매서드(재료 커맨드 설정 및 수정은 여기로!)
        {
            Queue<ConsoleKey> command = new Queue<ConsoleKey>();
            
            switch (type)
            {
                case IngredientType.Shot: //샷 커맨드 : 상 하
                    command.Enqueue(ConsoleKey.UpArrow);
                    command.Enqueue(ConsoleKey.DownArrow);                    

                    break;
                case IngredientType.Water: //물 커맨드 : 좌 우
                    command.Enqueue(ConsoleKey.LeftArrow);
                    command.Enqueue(ConsoleKey.RightArrow);

                    break;
                case IngredientType.Milk://우유 커맨드 : 좌 하 우
                    command.Enqueue(ConsoleKey.LeftArrow);
                    command.Enqueue(ConsoleKey.DownArrow);
                    command.Enqueue(ConsoleKey.RightArrow);
                    break;
                case IngredientType.Ice: //얼음 커맨드 : 상 상
                    command.Enqueue(ConsoleKey.UpArrow);
                    command.Enqueue(ConsoleKey.UpArrow);
                    break;
                case IngredientType.Chocolate: //초콜릿 커맨드 : 좌 우 좌
                    command.Enqueue(ConsoleKey.LeftArrow);
                    command.Enqueue(ConsoleKey.RightArrow);
                    command.Enqueue(ConsoleKey.LeftArrow);
                    break;
                case IngredientType.Strawberry: // 딸기 커맨드 : 우 좌 우
                    command.Enqueue(ConsoleKey.RightArrow);
                    command.Enqueue(ConsoleKey.LeftArrow);
                    command.Enqueue(ConsoleKey.RightArrow);
                    break;
            }

            return command;
        }

    }
}
