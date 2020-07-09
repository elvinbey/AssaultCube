using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Loader
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int key);
        private const int VK_F5 = 0x74;
        public static int hp = 100;
        public static string baseadress;
        static bool hack = false;
        static bool checkmulti()
        {
            int checkmultix = Trainer.ReadInteger(MyApi.Game.gamename, Int32.Parse(baseadress) + MyApi.Game.multi);
            if (checkmultix == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static bool checkeditor()
        {
            int checkeditorx = Trainer.ReadInteger(MyApi.Game.gamename, Int32.Parse(baseadress) + MyApi.Game.editor);
            if (checkeditorx == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Process process = Process.GetProcessesByName(MyApi.Game.gamename)[0];
                foreach (System.Diagnostics.ProcessModule moz in process.Modules)
                {
                    if ((moz.FileName.IndexOf("ac_client.exe") != -1))
                    {
                        baseadress = moz.BaseAddress.ToString();
                    }
                }
            }
            catch {
                Environment.Exit(0); 
            }
            
            while (true)
            {

                if (GetAsyncKeyState(VK_F5)  != 0)
                {
                    hack = !hack;
                }
                Console.Clear();
                if (checkmulti() == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Game Mode: Online!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game Mode: Offline!");
                }
                if (checkeditor() == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Editor Mod: True!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Editor Mod: False!");
                }
                if (hack == true)
                {
                    hp = hp + 200 * 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Hack(F5): True!");
                    Trainer.WritePointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset1, 999); // ult wp1
                    Trainer.WritePointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset2, 999); // ult wp2
                    Trainer.WritePointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset3, 999); // ult wp3
                   // Trainer.WritePointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset4, 1);   // auto wp
                    Trainer.WritePointerInteger(MyApi.Game.gamename, MyApi.Misc.hp, MyApi.Misc.offset5, hp);     // hp (offline)
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hack(F5): False!");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Base Address: " + baseadress);
                Console.WriteLine("Player Address: " + MyApi.Game.player);
                Console.WriteLine("Player Address: " + MyApi.Game.player);
                Console.WriteLine("Player Position X: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Game.player, MyApi.Game.position1));
                Console.WriteLine("Player Position Y: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Game.player, MyApi.Game.position2));
                Console.WriteLine("Player Position Z: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Game.player, MyApi.Game.position3));
                Console.WriteLine("Player Hp: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Game.player, MyApi.Game.hp));
                Console.WriteLine("Player Armor: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Game.player, MyApi.Game.arrmor));
                Console.WriteLine("Weapon1: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset1) + "/" + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset1_1));
                Console.WriteLine("Weapon2: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset2) + "/" + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset2_1));
                Console.WriteLine("Bomb: " + Trainer.ReadPointerInteger(MyApi.Game.gamename, MyApi.Misc.wpoint, MyApi.Misc.offset3));
                System.Threading.Thread.Sleep(150);
            }

        }
    }
}





namespace MyApi
{
    class Game
    {
        public static string gamename = "ac_client";
        public static int multi = 0x10F414;
        public static int editor = 0x110068;
        public static int player = 0x50F4F4;

        public static int[] position1 = new int[] { 0x4 };
        public static int[] position2 = new int[] { 0x8 };
        public static int[] position3 = new int[] { 0xC };


        public static int[] hp = new int[] { 0xF8 };
        public static int[] arrmor = new int[] { 0xFC };
    }

    class Misc
    {

        public static int wpoint = 0x0050F4F4;
        public static int[] offset1 = new int[] { 0x37c, 0x14 ,0x0}; // SİLAH
        public static int[] offset1_1 = new int[] { 0x360, 0x10, 0x0 }; // SİLAH

        public static int[] offset2 = new int[] { 0x34c, 0x14, 0x0 }; // TABANCA
        public static int[] offset2_1 = new int[] { 0x34c, 0x10, 0x0 }; // TABANCA

        public static int[] offset3 = new int[] { 0x368, 0x14, 0x0 }; // BOMBA
        public static int[] offset4 = new int[] { 0x36c, 0x18, 0xa0 }; // AUTO ON: 1

        public static int hp = 0x0050A280;
        public static int[] offset5 = new int[] { 0x60, 0x498, 0xf8 }; // HP
 
    }
}