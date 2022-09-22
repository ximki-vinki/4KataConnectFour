using System;
using System.Collections.Generic;
using System.Linq;

namespace _4KataConnectFour
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> myList = new List<string>()
            {
               "A_Yellow",
                "B_Red",
                "B_Yellow",
                "C_Red",
                "G_Yellow",
                "C_Red",
                "C_Yellow",
                "D_Red",
                "G_Yellow",
                "D_Red",
                "G_Yellow",
                "D_Red",
                "F_Yellow",
                "E_Red",
                "D_Yellow"
            };

            Console.WriteLine(ConnectFour.WhoIsWinner(myList));
        }
    }
    public class ConnectFour
    {
        //                                              Add drops  1, 1, 0, 0, 0, 0,    <-  1

        private static int[][] board = new int[7][] {  new int[6]{ 0, 0, 0, 0, 0, 0 },
                                                       new int[6]{ 0, 0, 0, 0, 0, 0 } ,
                                                       new int[6]{ 0, 0, 0, 0, 0, 0 } ,
                                                       new int[6]{ 0, 0, 0, 0, 0, 0 } ,
                                                       new int[6]{ 0, 0, 0, 0, 0, 0 } ,
                                                       new int[6]{ 0, 0, 0, 0, 0, 0 } ,
                                                       new int[6]{ 0, 0, 0, 0, 0, 0 } };

        private static readonly int redDrop = 1;
        private static readonly int yelowDrop = -1;

        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            if (piecesPositionList.Count < 8) return "Draw";
            for (int i = 0; i < 7; i++)
            {
                AddDrop(piecesPositionList[i]);
            }
            for (int i = 7; i < piecesPositionList.Count; i++)
            {
                (int column, int row) number = CalcNumberColumnAndRow(piecesPositionList[i]);
                AddDrop(piecesPositionList[i]);
                if (CheckForWin.Check(number.column, number.row))
                {
                    return piecesPositionList[i].Substring(2);
                }
            }

            return "Draw";
        }
        private static (int, int) CalcNumberColumnAndRow(string move)
        {
            int numberColumn = move[0] - 65;

            int numberRow = board[numberColumn].Count(x => x != 0);

            return (numberColumn, numberRow);
        }
        private static int CalcValueBaseColor(string move)
        {
            if (move[2] == 'R')
            {
                return redDrop;
            }
            return yelowDrop;
        }

        private static void AddDrop(string move)
        {
            (int column, int row) number = CalcNumberColumnAndRow(move);

            board[number.column][number.row] = CalcValueBaseColor(move);
        }

        private class CheckForWin
        {
            private static (int column, int row) _number;

            public static bool Check(int column, int row)
            {
                _number.column = column;

                _number.row = row;


                return (CountMatchLeftUp() + CountMatchRightDown()) > 2
                        || (CountMatchLeftDown() + CountMatchRightUp()) > 2
                        || (CountMatchDown() + CountMatchUp()) > 2
                        || CheckWinLeft();
            }

            private static int CountMatchLeftUp()
            {
                int countMatch = 0;
                for (int i = 0; i < _number.column && i < _number.row; i++)
                {
                    if (board[_number.column][_number.row] == board[_number.column - 1 - i][_number.row - 1 - i])
                    {
                        countMatch++;
                    }
                    else break;
                }
                return countMatch;
            }
            private static int CountMatchRightDown()
            {
                int countMatch = 0;
                for (int i = 0; _number.column + i + 1 < 7 && _number.row + i + 1 < 6; i++)
                {
                    if (board[_number.column][_number.row] == board[_number.column + 1 + i][_number.row + 1 + i])
                    {
                        countMatch++;
                    }
                    else break;
                }
                return countMatch;
            }
            private static int CountMatchLeftDown()
            {
                int countMatch = 0;
                for (int i = 0; _number.column + i + 1 < 7 && i < _number.row; i++)
                {
                    if (board[_number.column][_number.row] == board[_number.column + 1 + i][_number.row - 1 - i])
                    {
                        countMatch++;
                    }
                    else break;
                }
                return countMatch;
            }
            private static int CountMatchRightUp()
            {
                int countMatch = 0;
                for (int i = 0; i < _number.column && _number.row + i + 1 < 6; i++)
                {
                    if (board[_number.column][_number.row] == board[_number.column - 1 - i][_number.row + 1 + i])
                    {
                        countMatch++;
                    }
                    else break;
                }
                return countMatch;
            }
            private static bool CheckWinLeft()
            {

                if (_number.row < 3) return false;
                for (int i = _number.row; i > _number.row - 3; i--)
                {
                    if (board[_number.column][i] != board[_number.column][i - 1])
                    {
                        return false;
                    }
                }
                return true;
            }
            private static int CountMatchUp()
            {
                int countMatch = 0;
                for (int i = 0; i < _number.column ; i++)
                {
                    if (board[_number.column][_number.row] == board[_number.column - 1 - i][_number.row])
                    {
                        countMatch++;
                    }
                    else break;
                }
                return countMatch;
            }
            private static int CountMatchDown()
            {
                int countMatch = 0;
                for (int i = 0; _number.column + i + 1 < 7; i++)
                {
                    if (board[_number.column][_number.row] == board[_number.column + 1 + i][_number.row])
                    {
                        countMatch++;
                    }
                    else break;
                }
                return countMatch;

            }


        }
    }

}

