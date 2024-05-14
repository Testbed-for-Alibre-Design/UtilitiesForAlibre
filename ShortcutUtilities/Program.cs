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
		private static readonly string[] Modifiers = {
			"Ctrl", "Alt", "Shift", "Win",
			"Ctrl+Alt", "Ctrl+Shift", "Alt+Shift", "Ctrl+Alt+Shift",
			"Ctrl+Win", "Alt+Win", "Shift+Win",
			"Ctrl+Alt+Win", "Ctrl+Shift+Win", "Alt+Shift+Win",
			"Ctrl+Alt+Shift+Win"
		};
		private static readonly string[] NonModifiers = {
			"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
			"N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
			"Esc", "Tab", "Space", "Backspace", "Enter", "PageUp", "PageDown", "Home", "End", "Delete"
		};
		public static void Run()
		{
			var shortcutCombinations = GenerateShortcutCombinations();
			ProcessShortcutCombinations(shortcutCombinations, combination => Console.WriteLine(combination));
			Console.WriteLine($"Total Combinations: {shortcutCombinations.Count}");
		}
		public static void SaveToTxt()
		{
			var shortcutCombinations = GenerateShortcutCombinations();
			string exeDir = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(exeDir, "ShortcutCombinations.txt");
			ProcessShortcutCombinations(shortcutCombinations, combination =>
			{
				using (StreamWriter writer = new StreamWriter(filePath))
				{
					writer.WriteLine(combination);
				}
			});
			Console.WriteLine($"Shortcut combinations saved to {filePath}");
		}
		private static List<string> GenerateShortcutCombinations()
		{
			var shortcutCombinations = new List<string>();
			foreach (var modifier in Modifiers)
			{
				foreach (var nonModifier in NonModifiers)
				{
					shortcutCombinations.Add($"{modifier} + {nonModifier}");
				}
			}
			return shortcutCombinations;
		}
		private static void ProcessShortcutCombinations(List<string> shortcutCombinations, Action<string> action)
		{
			foreach (var combination in shortcutCombinations)
			{
				action(combination);
			}
		}
	}
}