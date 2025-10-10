using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public class SceneManager
    {
        public static void StartSetting()
        {
            Console.SetBufferSize(180, 30);
            Console.SetWindowSize(180, 30); // 콘솔 창 크기 120x15로 설정        
            Console.CursorVisible = false; // 커서 숨김
        }

        public static void TitleScene()
        {
            int x = 25;
            int y = 5;
            ClearScreenByFilling();
            Console.SetCursorPosition(x, y);
            Console.WriteLine(" $$$$$$\\             $$$$$$\\   $$$$$$\\                               $$$$$$$\\  $$\\                                         $$\\ ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("$$  __$$\\           $$  __$$\\ $$  __$$\\                              $$  __$$\\ $$ |                                        $$ |");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("$$ /  \\__| $$$$$$\\  $$ /  \\__|$$ /  \\__|$$$$$$\\   $$$$$$\\            $$ |  $$ |$$ | $$$$$$\\   $$$$$$\\   $$$$$$$\\  $$$$$$\\  $$ |");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("$$ |      $$  __$$\\ $$$$\\     $$$$\\    $$  __$$\\ $$  __$$\\           $$$$$$$  |$$ |$$  __$$\\  \\____$$\\ $$  _____|$$  __$$\\ $$ |");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("$$ |      $$ /  $$ |$$  _|    $$  _|   $$$$$$$$ |$$$$$$$$ |          $$  ____/ $$ |$$$$$$$$ | $$$$$$$ |\\$$$$$$\\  $$$$$$$$ |\\__|");
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("$$ |  $$\\ $$ |  $$ |$$ |      $$ |     $$   ____|$$   ____|          $$ |      $$ |$$   ____|$$  __$$ | \\____$$\\ $$   ____|    ");
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("\\$$$$$$  |\\$$$$$$  |$$ |      $$ |     \\$$$$$$$\\ \\$$$$$$$\\ $$\\       $$ |      $$ |\\$$$$$$$\\ \\$$$$$$$ |$$$$$$$  |\\$$$$$$$\\ $$\\ ");
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine(" \\______/  \\______/ \\__|      \\__|      \\_______| \\_______|$  |      \\__|      \\__| \\_______| \\_______|\\_______/  \\_______|\\__|");

            Console.SetCursorPosition(x, y + 15);
            Console.WriteLine("Space bar : 게임 방법");
            Console.SetCursorPosition(x + 55, y + 15);
            Console.WriteLine("Enter : 게임 시작");
            Console.SetCursorPosition(x + 110, y + 15);
            Console.WriteLine("Esc : 게임 종료");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Enter:
                    Gamemanager.StartGame();
                    break;
                case ConsoleKey.Spacebar:
                    RuleScene();
                    break;
                case ConsoleKey.Escape:
                    Gamemanager.EndGame();
                    break;
            }
        }

        public static void GameOverScene()
        {
            ClearScreenByFilling();
            ReadyToShow();
            Console.WriteLine("Game Over");
            ReadyToShowUnder();
            Console.WriteLine("당신은 파산했습니다...");
            PleasePress();
            Console.WriteLine("Enter : 시작 화면으로");
            if (Console.ReadKey(true).Key == ConsoleKey.Enter) { }
            ClearScreenByFillingMiddle();
            TitleScene();

        }

        public static void RuleScene()
        {
            ClearScreenByFilling();
            int x = 20, y = 14;
            Console.SetCursorPosition(x, y);            
            Console.Write("Coffee, Please!는 손님의 주문에 맞추어 음료를 만들어 돈을 버는 게임입니다. 이 카페는 조여오는 임대료의 압박에서 얼마나 살아남을 수 있을까요?");
            PleasePress();
            Console.WriteLine("Space bar :  다음");
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar) { }
            ClearScreenByFilling();
            Console.SetCursorPosition(x, y);
            Console.WriteLine("손님이 주문한 음료에는 알맞은 레시피가 있습니다. 레시피에 맞게 재료를 순서대로 넣어주세요.");                   
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("재료에 할당된 방향 커맨드를 순서대로 누른 뒤, Space bar를 누르면 재료를 투입합니다.");            
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("모든 재료를 투입한 뒤에는 Enter 키를 눌러 손님에게 음료를 건네주세요.");
            PleasePress();
            Console.WriteLine("Space bar :  다음");
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar) { }
            ClearScreenByFilling();
            Console.SetCursorPosition(x, y);
            Console.WriteLine("현재 만들어야 할 음료의 레시피는 1번, 각 재료별로 할당된 방향 커맨드는 2번을 누르면 언제든지 열람할 수 있습니다.");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("마이너스가 된 소지금을 다음날 수익으로 갚지 못하면 게임 오버하게 됩니다.");
            PleasePress();
            Console.WriteLine("Space bar :  다음");
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar) { }
            ClearScreenByFilling();
            Console.SetCursorPosition(x, y);
            Console.WriteLine("잊지 마세요, 손님이 덧붙여 말하는 추가 재료는 항상 마지막에 투입해야 합니다. 그럼 행운을 빕니다!");            
            PleasePress();
            Console.WriteLine("Space bar :  시작 화면으로");
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar) { }
            ClearScreenByFillingMiddle();
            TitleScene();

            Thread.Sleep(10000);
        }

        public static void EmptyStore()
        {
            ClearScreenByFillingMiddle();
            int x = 20, y = 23;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.SetCursorPosition(x + 8, y + 1); Console.WriteLine("■"); Console.SetCursorPosition(x + 130, y + 1); Console.WriteLine("■");
            Console.SetCursorPosition(x + 8, y + 2); Console.WriteLine("■"); Console.SetCursorPosition(x + 130, y + 2); Console.WriteLine("■");
            Console.SetCursorPosition(x + 8, y + 3); Console.WriteLine("■"); Console.SetCursorPosition(x + 130, y + 3); Console.WriteLine("■");
            Console.SetCursorPosition(x + 8, y + 4); Console.WriteLine("■"); Console.SetCursorPosition(x + 130, y + 4); Console.WriteLine("■");

        }

        public static void FarCustomerScene()
        {
            EmptyStore();
            int x = 80, y = 6;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("       ■■■        ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("      ■■■■        ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("      ■■■■        ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("       ■■■        ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("     ■■■■■       ");
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("     ■■■■■       ");
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("     ■■■■■       ");
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("       ■■■        ");
            Thread.Sleep(1000);
        }
        public static void CloseCustomerScene()
        {
            EmptyStore();
            int x = 78, y = 13;
            Console.SetCursorPosition(x, y - 1);
            Console.WriteLine("          ■■■■■■       ");
            Console.SetCursorPosition(x, y);
            Console.WriteLine("        ■■■■■■■■       ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("      ■■■■■■■■■■      ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("       ■■■■■■■■■      ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("         ■■■■■■■      ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("     ■■■■■■■■■■■      ");
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("     ■■■■■■■■■■■     ");
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("     ■■■■■■■■■■■     ");
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("     ■■■■■■■■■■■     ");
            Console.SetCursorPosition(x, y + 8);
            Console.WriteLine("     ■■■■■■■■■■■     ");
            Console.SetCursorPosition(x, y + 9);
            Console.WriteLine("     ■■■■■■■■■■■      ");
        }

        public static void ReadyToTalk()
        {
            int x = 28, y = 14;
            Console.SetCursorPosition(x, y);
        }

        public static void ReadyToNarration()
        {
            int x = 120, y = 14;
            Console.SetCursorPosition(x, y);
        }
        public static void ReadyToShow()
        {
            int x = 85, y = 7;
            Console.SetCursorPosition(x, y);
        }

        public static void ReadyToShowUnder()
        {
            int x = 83, y = 9;
            Console.SetCursorPosition(x, y);
        }
        public static void PleasePress()
        {
            int x = 70, y = 25;
            Console.SetCursorPosition(x, y);
        }

        public static void ClearScreenByFillingMiddle()
        {
            for (int i = 0; i + 6 < Console.WindowHeight - 5; i++) // 텍스트가 나오는 아래쪽 6줄은 제외
            {
                Console.SetCursorPosition(0, i + 5);                       // 위쪽 5줄도 제외
                Console.Write(new string(' ', Console.WindowWidth));   // 공백으로 채우기
            }            
        }
        public static void ClearScreenByFilling()// 화면 전부 공백으로 채우는 매서드
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));   // 공백으로 채우기
            }            

        }

        public static void ClearThatLineByFilling(int y)// 화면 전부 공백으로 채우는 매서드
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write(new string(' ', Console.WindowWidth));   // 공백으로 채우기
            }
        }
        public static void RecipeBookScene()
        {
            int x = 120, y = 10;
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"현재 음료 : {Customer.DrinkToOrder.Name}");
            Console.SetCursorPosition(x, y += 2);
            
            for (int i = 0; i < Customer.DrinkToOrder.Recipe.Count; i++)
            {
                Console.SetCursorPosition(x, y += 2);
                Console.WriteLine($"{Customer.DrinkToOrder.Recipe[i].Name}");
            }
            Thread.Sleep( 2000 );
            Console.SetCursorPosition(x, y = 10);            
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("                                       ");
        }

        public static void CommandBookScene()
        {
            int x = 110, y = 10;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("재료 커맨드 목록");
            Console.SetCursorPosition(x, y += 2);
            Console.WriteLine("에스프레소 샷   : ↑ ↓");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("물             : ← →");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("우유           : ← ↓ →");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("얼음           : ↑ ↑");
            if (Gamemanager.Days >= 2)
            {
                Console.SetCursorPosition(x, y += 1);
                Console.WriteLine("우유 거품      : ↓ ↓");
                Console.SetCursorPosition(x, y += 1);
                Console.WriteLine("시럽           : ← ←");
                Console.SetCursorPosition(x, y += 1);
            }
            if (Gamemanager.Days >= 3)
            {
                Console.SetCursorPosition(x, y += 1);
                Console.WriteLine("말차           : ← → ←");
                Console.SetCursorPosition(x, y += 1);
                Console.WriteLine("딸기           : → ← →");
            }
            if (Gamemanager.Days >= 4)
            {
                Console.SetCursorPosition(x, y += 1);
                Console.WriteLine("초콜릿         : ↑ ← →");
            }
            if (Gamemanager.Days >= 5)
            {
                Console.SetCursorPosition(x, y += 1);
                Console.WriteLine("휘핑크림       : → →");
            }
            Thread.Sleep(3000);
            Console.SetCursorPosition(x, y = 3);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
            Console.SetCursorPosition(x, y += 1);
            Console.WriteLine("                                       ");
        }

        
    }
}
