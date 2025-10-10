using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using static Coffee_Please.IngredientFactory;

namespace Coffee_Please
{
    public enum IngredientType //재료 타입(재료 종류 추가 및 수정은 여기로!).
    {
        Shot, Water, Milk, Ice, //첫날 해금 재료
        MilkFoam, Syrup, //2일차 해금 재료
        Strawberry, Matcha, //3일차 해금 재료
        Chocolate, //4일차 해금 재료
        WheepingCream //5일차 해금 재료
    }

    public class IngredientFactory //재료 객체 생성을 전담할 팩토리 클래스.
    {
        public static Dictionary<IngredientType, Ingredient> Ingredients { get; set; }

        public static List<IngredientType> PlusType = new List<IngredientType>// 주문 시 추가 가능한 재료 지정은 여기로!
        {IngredientType.Ice, IngredientType.Syrup, IngredientType.WheepingCream};

        public static List<Ingredient> PlusIngredients { get; set; }

        public IngredientFactory()
        {
            Ingredients = new Dictionary<IngredientType, Ingredient>();
            PlusIngredients = new List<Ingredient>();
        }

        public class Ingredient //재료 클래스.
        {
            public IngredientType Type { get; set; }

            public string Name { get; set; }
            public int Price { get; set; }

            public List<ConsoleKey> Command { get; set; }

            public Ingredient MakeClone()
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Type = Type;
                ingredient.Name = Name;
                ingredient.Price = Price;
                ingredient.Command = Command;
                return ingredient;
            }


            public virtual void PutIngredient() //만드는 음료에 재료를 추가하는 매서드.
            {
                Player.Menu.Recipe.Add(this);
                Player.Money -= Price;
                RenderIngredient();
            }

            public virtual void RenderIngredient()
            {
                SceneManager.ReadyToShow();
                Console.Write("                                       ");
                SceneManager.ReadyToShow();
                Console.WriteLine($"{Name}  투입!");
            }
        }

        public class PlusIngredient : Ingredient //손님의 추가 요구사항에 해당하는 추가 재료 클래스.
        {
            public override void RenderIngredient()
            {
                SceneManager.ReadyToShow();
                Console.Write("                                       ");
                SceneManager.ReadyToShow();
                Console.WriteLine($"{Name}  추가로 투입!");
            }
        }

        public void MakeAllIngredients() //재료 객체 종류별로 생성하는 매서드
        {
            for (int i = 0; i < Enum.GetValues(typeof(IngredientType)).Length; i++) //모든 객체 타입을 순회하며
            {
                Ingredients.Add((IngredientType)i, MakeIngredient((IngredientType)i)); //재료 객체 생성하여 재료 목록에 추가.
                if (MakeIngredient((IngredientType)i) is PlusIngredient) //해당 타입이 추가재료 타입에도 있다면
                {
                    PlusIngredients.Add(MakeIngredient((IngredientType)i)); //추가재료 목록에도 추가.
                }
            }
        }


        private Ingredient MakeIngredient(IngredientType type) //재료 타입 입력하면 가격과 커맨드 할당해주는 매서드.
        {
            Ingredient ingredient;
            if (PlusType.Contains(type))
            {
                ingredient = new PlusIngredient();
            }
            else
            {
                ingredient = new Ingredient();
            }
            ingredient.Type = type;
            ingredient.Price = SetPrice(type);
            ingredient.Name = SetName(type);
            ingredient.Command = SetCommand(type);
            return ingredient;
        }
        private string SetName(IngredientType type)//재료에 출력용 이름 할당하는 매서드(재료의 출력용 텍스트 설정 및 수정은 여기로!)
        {
            string name = "";

            switch (type)
            {
                case IngredientType.Shot:
                    name = "에스프레소 샷";
                    break;
                case IngredientType.Water:
                    name = "물";
                    break;
                case IngredientType.Milk:
                    name = "우유";
                    break;
                case IngredientType.Ice:
                    name = "얼음";
                    break;
                case IngredientType.Chocolate:
                    name = "초콜릿";
                    break;
                case IngredientType.Strawberry:
                    name = "딸기";
                    break;
                case IngredientType.MilkFoam:
                    name = "우유거품";
                    break;
                case IngredientType.Syrup:
                    name = "시럽";
                    break;
                case IngredientType.WheepingCream:
                    name = "휘핑크림";
                    break;
                case IngredientType.Matcha:
                    name = "말차";
                    break;

            }

            return name;
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
                case IngredientType.MilkFoam:
                    ingredient.Price = 1;
                    break;
                case IngredientType.Syrup:
                    ingredient.Price = 2;
                    break;
                case IngredientType.WheepingCream:
                    ingredient.Price = 2;
                    break;
                case IngredientType.Matcha:
                    ingredient.Price = 3;
                    break;

            }

            return ingredient.Price;
        }
        private List<ConsoleKey> SetCommand(IngredientType type) //재료에 키 커맨드 할당하는 매서드(재료 커맨드 설정 및 수정은 여기로!)
        {
            List<ConsoleKey> command = new List<ConsoleKey>();

            switch (type)
            {
                case IngredientType.Shot: //샷 커맨드 : 상 하
                    command.Add(ConsoleKey.UpArrow);
                    command.Add(ConsoleKey.DownArrow);

                    break;
                case IngredientType.Water: //물 커맨드 : 좌 우
                    command.Add(ConsoleKey.LeftArrow);
                    command.Add(ConsoleKey.RightArrow);

                    break;
                case IngredientType.Milk://우유 커맨드 : 좌 하 우
                    command.Add(ConsoleKey.LeftArrow);
                    command.Add(ConsoleKey.DownArrow);
                    command.Add(ConsoleKey.RightArrow);
                    break;
                case IngredientType.Ice: //얼음 커맨드 : 상 상
                    command.Add(ConsoleKey.UpArrow);
                    command.Add(ConsoleKey.UpArrow);
                    break;
                case IngredientType.Matcha: //말차 커맨드 : 좌 우 좌
                    command.Add(ConsoleKey.LeftArrow);
                    command.Add(ConsoleKey.RightArrow);
                    command.Add(ConsoleKey.LeftArrow);
                    break;
                case IngredientType.Strawberry: // 딸기 커맨드 : 우 좌 우
                    command.Add(ConsoleKey.RightArrow);
                    command.Add(ConsoleKey.LeftArrow);
                    command.Add(ConsoleKey.RightArrow);
                    break;
                case IngredientType.MilkFoam: // 우유거품 커맨드 : 하 하
                    command.Add(ConsoleKey.DownArrow);
                    command.Add(ConsoleKey.DownArrow);
                    break;
                case IngredientType.Syrup: // 시럽 커맨드 : 좌 좌
                    command.Add(ConsoleKey.LeftArrow);
                    command.Add(ConsoleKey.LeftArrow);
                    break;
                case IngredientType.Chocolate: // 초콜릿 커맨드 : 상 좌 우
                    command.Add(ConsoleKey.RightArrow);
                    command.Add(ConsoleKey.LeftArrow);
                    command.Add(ConsoleKey.RightArrow);
                    break;
                case IngredientType.WheepingCream: // 휘핑크림 커맨드 : 우 우
                    command.Add(ConsoleKey.RightArrow);
                    command.Add(ConsoleKey.RightArrow);                    
                    break;

            }

            return command;
        }

        public static void ShowCommand(IngredientType type, int i)
        {
            if (Ingredients[type].Command[i] == ConsoleKey.LeftArrow || Ingredients[type].Command[i] == ConsoleKey.RightArrow || Ingredients[type].Command[i] == ConsoleKey.UpArrow || Ingredients[type].Command[i] == ConsoleKey.DownArrow)
            {
                Console.WriteLine($"{Ingredients[type].Command[i]}");
            }
        }

    }
}
