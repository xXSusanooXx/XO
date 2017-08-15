using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using XO.Model.DbModels;
using XO.Model.OtherModels;

namespace XO.Web.Hubs
{
    public class XOhub : Hub
    {
        static readonly List<User> Users = new List<User>();
        static readonly List<Game> Games = new List<Game>();
        public void Connect()
        {
            if (Users.Count < 2)
            {
                Users.Add(new User()
                {
                    ConnectionId = Context.ConnectionId,
                    Login = Context.User.Identity.Name
                });
            }

            if (Users.Count == 2)
            {
                var newGame = new Game(Users[0], Users[1]);
                Clients.Client(Users[0].ConnectionId).startGame(true, newGame.TurnTime/1000);
                Clients.Client(Users[1].ConnectionId).startGame(false, newGame.TurnTime/1000);

                newGame.GameFinished += GameFinished;
                Games.Add(newGame);
            }
        }

        private void GameFinished(bool xPlayerWon, Game sender)
        {
            if (xPlayerWon)
            {
                Clients.Client(sender.XPlayer.ConnectionId).win();
                Clients.Client(sender.OPlayer.ConnectionId).lose();
            }
            else
            {
                Clients.Client(sender.OPlayer.ConnectionId).win();
                Clients.Client(sender.XPlayer.ConnectionId).lose();
            }
        }

        public void MakeTurn(int x, int y)
        {
            var user = Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var game = Games.FirstOrDefault(g => g.XPlayer == user || g.OPlayer == user);
            var whomToSend = game.XPlayer == user ? game.OPlayer : game.XPlayer;
            Clients.Client(whomToSend.ConnectionId).opponentMadeTurn(x, y);
            game.MakeTurn(x, y, user);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}