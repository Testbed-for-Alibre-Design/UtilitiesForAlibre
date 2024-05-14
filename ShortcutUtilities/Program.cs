namespace ShortcutUtilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Usage: ShortcutUtilities <method>");
            Console.WriteLine("Available methods:");
            Console.WriteLine("  KeyboardShortcutGenerator.Run [run]");
            Console.WriteLine("  KeyboardShortcutGenerator.SaveToTxt [save]");
            //Console.WriteLine("  KeyboardShortcutGenerator.a [a]");
            //Console.WriteLine("  KeyboardShortcutGenerator.b [b]");
            //Console.WriteLine("  KeyboardShortcutGenerator.c [c]");
            //Console.WriteLine("  KeyboardShortcutGenerator.d [d]"); 
            //Console.WriteLine("  KeyboardShortcutGenerator.e [e]"); 
            var command = Console.ReadLine()?.ToLower();
            switch (command)
            {
                case "run":
                    KeyboardShortcutGenerator.Run();
                    break;
                case "save":
                    KeyboardShortcutGenerator.SaveToTxt();
                    break;
                //case "a":
                //    Console.WriteLine("a function not implemented yet.");
                //    break;
                //case "b":  
                //    Console.WriteLine("b function not implemented yet.");
                //    break;
                //case "c": 
                //    Console.WriteLine("c function not implemented yet.");
                //    break;
                //case "d":  
                //    Console.WriteLine("d function not implemented yet.");
                //    break;
                //case "e":  
                //    Console.WriteLine("e function not implemented yet.");
                //    break;
                default:
                    Console.WriteLine($"Unknown method: {args[0]}");
                    break;
            }
        }

    }
    static class KeyboardShortcutGenerator
    {
        public static void Run()
        {
            string[] modifiers = {
            "Ctrl", "Alt", "Shift", "Win",
            "Ctrl+Alt", "Ctrl+Shift", "Alt+Shift", "Ctrl+Alt+Shift",
            "Ctrl+Win", "Alt+Win", "Shift+Win",
            "Ctrl+Alt+Win", "Ctrl+Shift+Win", "Alt+Shift+Win",
            "Ctrl+Alt+Shift+Win"
        };
            string[] nonModifiers = {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
            "Esc", "Tab", "Space", "Backspace", "Enter", "PageUp", "PageDown", "Home", "End", "Delete"
        };
            List<string> shortcutCombinations = new List<string>();
            foreach (var modifier in modifiers)
            {
                foreach (var nonModifier in nonModifiers)
                {
                    shortcutCombinations.Add($"{modifier} + {nonModifier}");
                }
            }
            foreach (var combination in shortcutCombinations)
            {
                Console.WriteLine(combination);
            }
            Console.WriteLine($"Total Combinations: {shortcutCombinations.Count}");
        }
        public static void SaveToTxt()
        {
            string[] modifiers = {
                "Ctrl", "Alt", "Shift", "Win",
                "Ctrl+Alt", "Ctrl+Shift", "Alt+Shift", "Ctrl+Alt+Shift",
                "Ctrl+Win", "Alt+Win", "Shift+Win",
                "Ctrl+Alt+Win", "Ctrl+Shift+Win", "Alt+Shift+Win",
                "Ctrl+Alt+Shift+Win"
            };
            string[] nonModifiers = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
                "Esc", "Tab", "Space", "Backspace", "Enter", "PageUp", "PageDown", "Home", "End", "Delete"
            };
            List<string> shortcutCombinations = new List<string>();
            foreach (var modifier in modifiers)
            {
                foreach (var nonModifier in nonModifiers)
                {
                    shortcutCombinations.Add($"{modifier} + {nonModifier}");
                }
            }
            string exeDir = AppDomain.CurrentDomain.BaseDirectory; // Get the executable directory
            string filePath = Path.Combine(exeDir, "ShortcutCombinations.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var combination in shortcutCombinations)
                {
                    writer.WriteLine(combination);
                }
                writer.WriteLine($"Total Combinations: {shortcutCombinations.Count}");
            }
            Console.WriteLine($"Shortcut combinations saved to {filePath}");
        }
    }
}
