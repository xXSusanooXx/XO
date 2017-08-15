using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using XO.Model.DbModels;

namespace XO.Model.OtherModels
{
    public class Game
    {
        public User XPlayer { get; private set; }
        public User OPlayer { get; private set; }
        public CellValue[,] Matrix { get; private set; }

        public int TurnTime { get; private set; }//in milliseconds
        private Timer _timer;

        public event Action<bool, Game> GameFinished;

        public Game(User xPlayer, User oPlayer)
        {
            XPlayer = xPlayer;
            OPlayer = oPlayer;
            Matrix = new CellValue[15, 15];
            TurnTime = 20000;
        }

        public void MakeTurn(int x, int y, User user)
        {
            var isXPlayerTurn = user == XPlayer;
            Matrix[y, x] = isXPlayerTurn ? CellValue.X : CellValue.O;

            var isFinalTurn = CheckField();

            if (isFinalTurn)
            {
                GameFinished(isXPlayerTurn, this);
                _timer.Stop();
                _timer.Dispose();
                return;
            }

            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            _timer = new Timer();
            _timer.Interval = TurnTime;

            _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                GameFinished(isXPlayerTurn, this);
                _timer.Stop();
                _timer.Dispose();
            };
            _timer.Start();
        }

        private bool CheckField()
        {
            var count = 0;
            var cell = CellValue.None;
            for (var i = 0; i < 15; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var currentCell = Matrix[i, j];

                    if (currentCell == CellValue.None)
                    {
                        cell = CellValue.None;
                        count = 0;
                        continue;
                    }

                    if (currentCell == cell || cell == CellValue.None)
                        count++;
                    else
                    {
                        count = 1;
                    }

                    if (count == 5) return true;
                }
                count = 0;
            }
            for (var i = 0; i < 15; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var currentCell = Matrix[j, i];

                    if (currentCell == CellValue.None)
                    {
                        cell = CellValue.None;
                        count = 0;
                        continue;
                    }

                    if (currentCell == cell || cell == CellValue.None)
                        count++;
                    else
                    {
                        count = 1;
                    }

                    if (count == 5) return true;
                }
                count = 0;
            }
            //for (var i = 0; i < 15; i++)
            //{
            //    for (var j = 0; j < 15; j++)
            //    {
            //        var currentCell = Matrix[i, j];

            //        if (count == 5) return true;
            //    }
            //}

            return false;
        }

        public enum CellValue
        {
            None,
            X,
            O
        }
    }


}
