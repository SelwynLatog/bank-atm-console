using System;

public class MenuSystem
{
    private string[] options;
    private int selectedIndex;
    private string? prompt;

    public MenuSystem(string[] menuOptions, string? menuPrompt = null)
    {
        options = menuOptions;
        selectedIndex = 0;
        prompt = menuPrompt;
    }

    public int ShowMenu()
    {
        Console.CursorVisible = false;

        while (true)
        {
            DrawMenu();
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = selectedIndex > 0 ? selectedIndex - 1 : options.Length - 1;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = selectedIndex < options.Length - 1 ? selectedIndex + 1 : 0;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.CursorVisible = true;
                Console.WriteLine();
                return selectedIndex;
            }
        }
    }

    private void DrawMenu()
    {
        Console.Clear();

        if (!string.IsNullOrEmpty(prompt))
        {
            Console.WriteLine(prompt);
            Console.WriteLine();
        }

        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(">");
                Console.Write(options[i]);
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.Write(" ");
                Console.Write(options[i]);
                Console.WriteLine();
            }
        }
    }
}

public static class MenuHelper
{
    public static int ShowMenu(string[] options, string? prompt = null)
    {
        MenuSystem menu = new MenuSystem(options, prompt);
        return menu.ShowMenu();
    }

    public static bool ShowConfirmation(string? prompt = null)
    {
        int choice = ShowMenu(MenuOptions.YES_NO, prompt);
        return choice == 0;
    }

    public static int ShowLogin(string? prompt = null)
    {
        return ShowMenu(MenuOptions.LOGIN_MENU, prompt);
    }

    public static int ShowMainMenu(string? prompt = null)
    {
        return ShowMenu(MenuOptions.MAIN_MENU, prompt);
    }
}