using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGame
{
    internal class BoxGame
    {
        string letters = "abcde";
        public BoxGame()
        {
         
        }


        //menu
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Box Game!");
            Console.ReadKey();
            Console.WriteLine("Please enter a single character for Player 1.");
            string player1 = Console.ReadLine();
            Console.WriteLine("Welcome " + player1 + "! Please enter a single character for Player 2.");
            string player2 = Console.ReadLine();
            Console.WriteLine("Welcome " + player2 + "! Enjoy!");
            BoxGame currentGame = new BoxGame();
            string[] board = currentGame.CreateBoard();
            currentGame.PrintBoard(board);
            Console.ReadKey();

            int player1Score = currentGame.PlayerScore(player1, board);
            int player2Score = currentGame.PlayerScore(player2, board);


            Boolean isPlayer1Turn = true;
           

            do
            {
                //player turn
                //tally and print score
                //switch player turn
                if (isPlayer1Turn)
                {
                    board = currentGame.PlayerTurn(player1, board, currentGame);
                    board = currentGame.CloseBoxes(player1, board, player1Score);
                }
                else
                {
                    board = currentGame.PlayerTurn(player2, board, currentGame);
                    board = currentGame.CloseBoxes(player2, board, player2Score);

                }
                player1Score = currentGame.PlayerScore(player1, board);
                player2Score = currentGame.PlayerScore(player2, board);
                Console.WriteLine(player1 + " score: " + player1Score + ", " + player2 + " score: " + player2Score);
                isPlayer1Turn = !isPlayer1Turn;

            } while (player1Score + player2Score < 16);






            Console.ReadKey();


        }


        //        create a board
        public string[] CreateBoard()
        {
            string[] board = {"a1--a2  a3  a4  a5",
                "|   |             ",
                "b1  b2  b3  b4  b5",
                "                  ",
                "c1  c2  c3  c4  c5",
                "                  ",
                "d1  d2  d3  d4  d5",
                "                  ",
                "e1  e2  e3  e4  e5"};
            return board;

        }





        //add a line to a board
        public string[] LineOnBoard(string lineLocationStart, string lineLocationEnd, string[] board)
        {
            int row = letters.IndexOf(lineLocationStart[0]) * 2;
            int column = (int)Char.GetNumericValue(lineLocationStart[1]);
            if (lineLocationStart[0] == lineLocationEnd[0])
            {
                board[row] = board[row].Remove((4 * column - 2), 2);
                board[row] = board[row].Insert((4 * column - 2), "--");
            }
            else
            {
                board[row + 1] = board[row + 1].Remove(4 * (column - 1), 1);
                board[row + 1] = board[row + 1].Insert(4 * (column - 1), "|");
            }
            return board;
        }



        //add initial to a closed box
        public string[] CloseBoxes(string player, string[] board, int score)
        {
            int runningScore = score;
            Boolean areBoxesLeft = true;

            while (areBoxesLeft)
            {
                for (int x = 0; x <= 16; x++)
                {
                    for (int i = 0; i < board.Length - 2; i = i + 2)
                    {
                        for (int j = 2; j < board[i].Length; j = j + 4)
                        {
                            if (board[i].Substring(j, 1).Equals("-") && board[i + 1].Substring(j + 2, 1).Equals("|") && board[i + 2].Substring(j, 1).Equals("-") && board[i + 1].Substring(j - 2, 1).Equals("|"))
                            {
                                if (board[i+1].Substring(j, 1).Equals(" "))
                                {
                                    board[i + 1] = board[i + 1].Remove(j, 1);
                                    board[i + 1] = board[i + 1].Insert(j, player);
                                    runningScore++;
                                }
                                
                            }
                        }
                    }
                }
                if (score == runningScore)
                {
                    areBoxesLeft = false;
                }
                else
                {
                    score = runningScore;
                }
            }
            return board;
        }



            
        



        //tally initials
        public int PlayerScore(string player, string[] board)
        {
            int score = 0;
            foreach (string row in board)
            {
                foreach (char c in row)
                {
                    if (char.Parse(player) == c) {
                        score++;
                    }
                }
            }
            return score;
        }





        //print a board
        public void PrintBoard(string[] board)
        {
            foreach (string row in board)
            {
                Console.WriteLine(row);
            }
        }




        //takes a turn
        public string[] PlayerTurn(string player, string[] board, BoxGame currentGame)
        {
            Console.WriteLine(player + ", please choose two adjacent spaces to connect.");
            Console.WriteLine("Starting location:");
            string startingLocation = Console.ReadLine();
            Console.WriteLine("Ending location:");
            string endingLocation = Console.ReadLine();
            board = currentGame.LineOnBoard(startingLocation, endingLocation, board);
            currentGame.PrintBoard(board);
            return board;
        }
    }
}

