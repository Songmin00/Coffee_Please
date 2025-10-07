using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public class Gamemanager
    {
        public static List<ConsoleKey> AvailableConsoleKeys {  get; set; }

        public Gamemanager()
        {
            KeySetting();
        }

        public void KeySetting()
        {
            AvailableConsoleKeys = new List<ConsoleKey>();
            AvailableConsoleKeys.Add(ConsoleKey.UpArrow);
            AvailableConsoleKeys.Add(ConsoleKey.DownArrow);
            AvailableConsoleKeys.Add(ConsoleKey.LeftArrow);
            AvailableConsoleKeys.Add(ConsoleKey.RightArrow);
            AvailableConsoleKeys.Add(ConsoleKey.Spacebar);
        }

        static void HandleInput()
        {
            if (Console.KeyAvailable) // 키가 눌렸는지 확인
            {
                var key = Console.ReadKey(true); // 키 입력 읽기, true = 콘솔에 표시 안함

                if (AvailableConsoleKeys.Contains(key.Key))
                {

                }
              
            }
        }
    }
}
