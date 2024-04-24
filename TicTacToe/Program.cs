char[] arr = ['0', '1', '2', '3', '4', '5', '6', '7', '8'];
int[] availableMoves = [0, 1, 2, 3, 4, 5, 6, 7, 8];
bool usersTurn = Coinflip();

if (usersTurn)
{
    Board(arr);
}


while (true)
{
    if (!usersTurn)
    {
        Thread.Sleep(2000);

        (int, int[]) index =  ComputersTurn(availableMoves);
        availableMoves = index.Item2;
        arr[index.Item1] = 'O';
        Console.Clear();
        Board(arr);
        Console.WriteLine("The Computer Chose {0}", index.Item1);
        usersTurn = !usersTurn;
        if (Check(arr))
        {
            Console.WriteLine("Computer Wins!");
            break;
        }
    }
    else
    {
        (int, int[]) index = PlayersTurn(availableMoves);
        arr[index.Item1] = 'X';
        availableMoves = index.Item2;
        Console.Clear();
        Board(arr);
        usersTurn = !usersTurn;
        if (Check(arr))
        {
            Console.WriteLine("You Win!");
            break;
        }
    }

    if (availableMoves.Length == 0)
    {
        Console.WriteLine("Draw!");
        break;
    }
}


static void Board(char[] arr)
{
    Console.WriteLine();
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", arr[0], arr[1], arr[2]);
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", arr[3], arr[4], arr[5]);
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", arr[6], arr[7], arr[8]);
    Console.WriteLine("     |     |      ");
}

static bool Coinflip()
{
    Random rnd = new Random();
    bool userBegins;
    
    Console.WriteLine("heads or tails?");
    String coin = Console.ReadLine()!;

    while (true)
    {
        if (coin.Equals("heads") || coin.Equals("tails"))
        {
            break;
        }
        else
        {
            Console.WriteLine("Wrong Input, write 'heads' or 'tails'");
            coin = Console.ReadLine()!;
        }
    }

    Console.WriteLine("Flipping a coin");
    Thread.Sleep(2000);
    int num = rnd.Next(1, 3);
    string resultCoin = num == 1 ? "heads" : "tails";

    if (coin.Equals(resultCoin))
    {
        userBegins = true;
        Console.WriteLine("You Chose {0} and The Coin was {1}, You Choose first", coin, resultCoin);
    }
    else
    {
        userBegins = false;
        Console.WriteLine("You Chose {0} and The Coin was {1}, Computer Chooses first", coin, resultCoin);
    }

    return userBegins;
}

static (int, int[]) ComputersTurn(int[] availableMoves)
{
    Random rnd = new Random();
    
    int move = availableMoves[rnd.Next(0, availableMoves.Length)];
    availableMoves = RemoveIndexFromAvailableMoves(availableMoves, move);
    
    return (move, availableMoves);
}

static (int, int[]) PlayersTurn(int[] availableMoves)
{
    Console.WriteLine("Make a move");
    String moveString = Console.ReadLine()!;
    
    while (true)
    {
        if (Int32.TryParse(moveString, out int move))
        {
            if (availableMoves.Contains(move))
            {
                availableMoves = RemoveIndexFromAvailableMoves(availableMoves, move);
                return (move, availableMoves);
            }
            else
            {
                Console.WriteLine("The Choice {0} is Not Available, Choose again", move);
                moveString = Console.ReadLine()!;
            }
        }
        else
        {
            Console.WriteLine("Input Has To Be An Available Integer");
            moveString = Console.ReadLine()!;
        }
    }
}

bool Check(char[] arr)
{
    int[][] winningConditions =
    [
        [0, 1, 2], [3, 4, 5], [6, 7, 8],
        [0, 3, 6], [1, 4, 7], [2, 5, 8],
        [0, 4, 8], [2, 4, 6]
    ];
    return winningConditions.Any(wc => wc.All(index => arr[index] == arr[wc[0]]));
}

static int[] RemoveIndexFromAvailableMoves(int[] availableMoves, int move)
{
    var removeIndex = Array.IndexOf(availableMoves, move);
    var foos = new List<int>(availableMoves);
    foos.RemoveAt(removeIndex);
    availableMoves = foos.ToArray();
    return availableMoves;
}