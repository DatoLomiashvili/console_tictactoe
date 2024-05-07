char[] board = ['0', '1', '2', '3', '4', '5', '6', '7', '8'];
int[] availableMoves = [0, 1, 2, 3, 4, 5, 6, 7, 8];
bool usersTurn = Coinflip();

if (usersTurn)
{
    Board(board);
}


while (true)
{
    if (!usersTurn)
    {
        Thread.Sleep(2000);

        (int, int[]) index =  ComputersTurn(availableMoves, board);
        availableMoves = index.Item2;
        board[index.Item1] = 'O';
        Console.Clear();
        Board(board);
        Console.WriteLine("The Computer Chose {0}", index.Item1);
        usersTurn = !usersTurn;
        if (CheckWin(board))
        {
            Console.WriteLine("Computer Wins!");
            break;
        }
    }
    else
    {
        (int, int[]) index = PlayersTurn(availableMoves);
        board[index.Item1] = 'X';
        availableMoves = index.Item2;
        Console.Clear();
        Board(board);
        usersTurn = !usersTurn;
        if (CheckWin(board))
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


static void Board(char[] board)
{
    Console.WriteLine();
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", board[0], board[1], board[2]);
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", board[3], board[4], board[5]);
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", board[6], board[7], board[8]);
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

static (int, int[]) ComputersTurn(int[] availableMoves, char[] board)
{
    int? move = null;
    Random rnd = new Random();
    char[] boardCopy = [];
    for (int i = 0; i < availableMoves.Length; i++)
    {
        boardCopy = new char[board.Length];
        Array.Copy(board, boardCopy, board.Length);
        boardCopy[availableMoves[i]] = 'O';
        if (CheckWin(boardCopy, 'O'))
        {
            move = availableMoves[i];
            break;
        };
    }
    
    for (int i = 0; i < availableMoves.Length; i++)
    {
        boardCopy = new char[board.Length];
        Array.Copy(board, boardCopy, board.Length);
        boardCopy[availableMoves[i]] = 'X';
        if (CheckWin(boardCopy, 'X'))
        {
            move = availableMoves[i];
            break;
        };
    }
    
    if (board[4] == '4' && move == null)
    {
        move = 4;
    }
    
    int computerMove = move ?? availableMoves[rnd.Next(0, availableMoves.Length)];
    Console.WriteLine("Computer Move:" + computerMove);
    availableMoves = RemoveIndexFromAvailableMoves(availableMoves, computerMove);
    
    return (computerMove, availableMoves);
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

static bool CheckWin(char[] board, char? symbol = null)
{
    int[][] winningConditions =
    [
        [0, 1, 2], [3, 4, 5], [6, 7, 8],
        [0, 3, 6], [1, 4, 7], [2, 5, 8],
        [0, 4, 8], [2, 4, 6]
    ];
    return winningConditions.Any(wc => wc.All(index => board[index] == (symbol ?? board[wc[0]])));
}

static int[] RemoveIndexFromAvailableMoves(int[] availableMoves, int move)
{
    var removeIndex = Array.IndexOf(availableMoves, move);
    var foos = new List<int>(availableMoves);
    foos.RemoveAt(removeIndex);
    availableMoves = foos.ToArray();
    return availableMoves;
}