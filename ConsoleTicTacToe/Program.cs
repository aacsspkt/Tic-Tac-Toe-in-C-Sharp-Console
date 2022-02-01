// See https://aka.ms/new-console-template for more informationa

var board = new Board();
do
{
    board.run_game();
} while (board.is_not_complete());


class Board
{
    public enum GameStatus
    {
        DRAW,
        NOT_COMPLETE,
        COMPLETE
    }
    public enum GameTurn
    {
        Player_1,
        Player_2
    }

    //tictactoe cell init
    char[] cell = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    // init game error with empty string
    String game_error = "";

    // init turn as 1
    GameTurn turn = GameTurn.Player_1;    // init status as GameStatus.NOT_COMPLETE 
    GameStatus status = GameStatus.NOT_COMPLETE;

    public bool is_not_complete()
    {
        return status == GameStatus.NOT_COMPLETE;
    }

    public void run_game()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nWelcome to TicTacToe!");
        Console.WriteLine("Player 1: X | Player 2: O");
        Console.ResetColor();
        
        Console.WriteLine("\n");
        Console.WriteLine($"     {cell[1]} | {cell[2]} | {cell[3]}");
        Console.WriteLine("   ----+----+----");
        Console.WriteLine($"     {cell[4]} | {cell[5]} | {cell[6]}");
        Console.WriteLine("   ----+----+----");
        Console.WriteLine($"     {cell[7]} | {cell[8]} | {cell[9]}");
        Console.WriteLine("\n");

        check_status();
        print_instructions();
        try
        {
            int input = Convert.ToInt16(Console.ReadLine());
            if (input_is_valid(input))
                store_value(input);
        }
        catch (FormatException fe)
        {
            game_error = fe.Message;
        }
    }

    // return true if input is valid
    private bool input_is_valid(int input)
    {
        if (input < 1 | input > 9)
        {
            game_error = "Invalid Input!: Value must be within 1 to 9.";
            return false;
        }
        if (cell_has_value(input))
        {
            game_error = "Cell is already occupied!";
            return false;
        }
        return true;
    }

    // print instructions for user
    public void print_instructions()
    {
        if (game_error != "")
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(game_error);
            Console.ResetColor();
            game_error = "";
        }
        string player = turn == GameTurn.Player_1 ? "Player 1 turn." : "Player 2 turn.";
        Console.WriteLine(player);
        Console.Write("Enter cell number for input from 1 to 9:  ");
    }

    // store user input
    private void store_value(int input)
    {
        if (turn.Equals(GameTurn.Player_1))
        {
            cell[input] = 'X';
            turn = GameTurn.Player_2;
        }
        else
        {
            cell[input] = 'O';
            turn = GameTurn.Player_1;
        }
    }

    // check cell has value or not
    private bool cell_has_value(int index)
    {
        return cell[index] == 'X' || cell[index] == 'O';
    }


    // check game is completed or not and has winner or not
    // return game status
    private void check_status()
    {
        // check horizontal rows
        if (cell[1] == cell[2] && cell[2] == cell[3])
        {
            status = GameStatus.COMPLETE;
            declare_winner();
        }
        else if (cell[4] == cell[5] && cell[5] == cell[6])
        {
            declare_winner();
            status = GameStatus.COMPLETE;
        }
        else if (cell[7] == cell[8] && cell[8] == cell[9])
        {
            declare_winner();
            status = GameStatus.COMPLETE;
        }

        // check vertical columns
        else if (cell[1] == cell[4] && cell[4] == cell[7])
        {
            status = GameStatus.COMPLETE;
            declare_winner();
        }
        else if (cell[2] == cell[5] && cell[5] == cell[8])
        {
            status = GameStatus.COMPLETE;
            declare_winner();
        }
        else if (cell[3] == cell[6] && cell[6] == cell[9])
        {
            status = GameStatus.COMPLETE;
            declare_winner();
        }

        // check diagonals
        else if (cell[1] == cell[5] && cell[5] == cell[9])
        {
            status = GameStatus.COMPLETE;
            declare_winner();
        }
        else if (cell[3] == cell[5] && cell[5] == cell[7])
        {
            status = GameStatus.COMPLETE;
            declare_winner();
        }

        else if (cell[1] != '1' && cell[2] != '2' && cell[3] != '3' && cell[4] != '4' && cell[5] != '5' && cell[6] != '6' && cell[7] != '7' && cell[8] != '8' && cell[9] != '9')
        {
            status = GameStatus.DRAW;
            declare_draw();
        }
    }

    private void declare_draw()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Game is draw!");
        Console.ResetColor();

        ask_for_new_game();
    }

    private void declare_winner()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        // if it is player 2's turn then Player 1 was winner
        if (turn == GameTurn.Player_2)
            Console.WriteLine("The winner is Player First.\n");
        else
            Console.WriteLine("Game Completed.The winner is Player Second.\n");
        Console.ResetColor();

        ask_for_new_game();
    }

    private void ask_for_new_game()
    {
        Console.Write("Do you want to play again? [y/N]");
        string? input = Console.ReadLine();
        if (input == "y" || input == "Y")
            restartGame();
    }

    private void restartGame()
    {
        cell = new char[9];
        status = GameStatus.NOT_COMPLETE;
        turn = GameTurn.Player_1;
        run_game();
    }
}

