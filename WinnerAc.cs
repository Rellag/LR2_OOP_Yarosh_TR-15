using System;
namespace LR2
{
    public class WinnerAc : GameAccount
    {
        public WinnerAc(string userName, int сurrentRating, int gamesCount) : base(userName, сurrentRating, gamesCount)
        {
        }

        public override void WinGame(GameAccount opponent, Game game)
        {
            if (game.addRating < 0 || game.addOpponentRating < 0)
            {
                throw new ArgumentOutOfRangeException("Game rating can not be negative");
            }

            if (game.addRating >= CurrentRating && game.addOpponentRating >= opponent.CurrentRating)
            {
                throw new ArgumentOutOfRangeException("Game rating is too high");
            }

            WinStreak++;
            opponent.WinStreak = 0;

            if(WinStreak >= 3)
            {
                game.addRating *= 2;
            }

            CurrentRating += game.addRating;

            GamesCount++;

            if (opponent is SafeLoserAc)
            {
                game.addOpponentRating /= 2;
            }

            opponent.CurrentRating -= game.addOpponentRating;




            PrintWin(opponent, game);


            AddToStatWin(opponent, game);

            game.chooseRating(game.type);

        }
    }
}

