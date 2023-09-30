using GameOfLife;

Console.WriteLine("Hello, World!");

var currentField = new bool[20, 20];
currentField[5, 5] = true;
currentField[6, 5] = true;
currentField[7, 5] = true;

// Game Loop
while (true)
{
    Paint(currentField);
    currentField = Game.NextStep(currentField);
    //Регулируемая скорость шага
    await Task.Delay(10);
}


void Paint(bool[,] field)
{
    Console.SetCursorPosition(0, 0);
    for (int y = 0; y < field.GetLength(1); y++)
    {
        for (int x = 0; x < field.GetLength(0); x++)
        {
            var symbol = field[x, y] ? '#' : ' ';
            Console.Write(symbol);
        }
        Console.WriteLine();
    }
}