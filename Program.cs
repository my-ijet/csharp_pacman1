using System;

internal class Program
{
  static bool shouldRun = true;
  static ConsoleKey pressedKey = new();
  static char[,] map = ReadMap("map.txt");

  static int pacmanX = 1;
  static int pacmanY = 1;

  static int score = 0;

  private static void Main(string[] args)
  {
    Task.Run(() =>
    {
      while (shouldRun)
      {
        pressedKey = Console.ReadKey().Key;
      }
    });

    Console.CursorVisible = false;
    while (shouldRun)
    {
      Console.Clear();
      HandleInput();

      DrawMap();
      DrawPlayer();
      DrawScore();

      Thread.Sleep(250);
    }

    Console.CursorVisible = true;

  }

  static void DrawScore()
  {
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.SetCursorPosition(0, map.GetLength(1));
    Console.WriteLine($"Score: {score}");
  }

  static void HandleInput()
  {
    int[] direction = new int[2];

    switch (pressedKey)
    {
      case ConsoleKey.UpArrow: direction[1] = -1; break;
      case ConsoleKey.DownArrow: direction[1] = 1; break;
      case ConsoleKey.LeftArrow: direction[0] = -1; break;
      case ConsoleKey.RightArrow: direction[0] = 1; break;
      case ConsoleKey.Escape or ConsoleKey.Q:
        shouldRun = false; return;
      default: break;
    }

    int pacmanXNext = pacmanX + direction[0];
    int pacmanYNext = pacmanY + direction[1];
    char mapChar = map[pacmanXNext, pacmanYNext];

    if (mapChar == '#' ||
      pacmanXNext < 0 || pacmanXNext >= map.GetLength(0) ||
      pacmanYNext < 0 || pacmanYNext >= map.GetLength(1))
      return;

    pacmanX = pacmanXNext;
    pacmanY = pacmanYNext;

    if (mapChar == '·')
    {
      score++;
      map[pacmanX, pacmanY] = ' ';
    }
  }

  static void DrawPlayer()
  {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.SetCursorPosition(pacmanX, pacmanY);
    Console.Write("@");
  }

  static void DrawMap()
  {
    Console.ForegroundColor = ConsoleColor.Blue;

    for (int y = 0; y < map.GetLength(1); y++)
    {
      for (int x = 0; x < map.GetLength(0); x++)
      {
        Console.Write(map[x, y]);
      }
      Console.WriteLine();
    }
  }

  static char[,] ReadMap(string path)
  {
    string[] file = File.ReadAllLines(path);
    char[,] map = new char[GetMaxLenghtOfLine(file), file.Length];

    for (int y = 0; y < map.GetLength(1); y++)
    {
      for (int x = 0; x < map.GetLength(0); x++)
      {
        map[x, y] = file[y][x];
      }
    }

    return map;
  }

  static int GetMaxLenghtOfLine(string[] lines)
  {
    int maxLenght = 0;

    foreach (var line in lines)
    {
      if (line.Length > maxLenght)
      {
        maxLenght = line.Length;
      }
    }

    return maxLenght;
  }
}
