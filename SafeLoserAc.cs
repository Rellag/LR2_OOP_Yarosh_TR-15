using System;
namespace LR2
{
    public class SafeLoserAc : GameAccount
    {
        public SafeLoserAc(string userName, int сurrentRating, int gamesCount) : base(userName, сurrentRating, gamesCount)
        {
        }

        public override void LoseGame(GameAccount opponent, Game game)
        {
            if (game.addRating < 0 || game.addOpponentRating < 0)
            {
                throw new ArgumentOutOfRangeException("Game rating can not be negative");
            }

            if (game.addRating >= CurrentRating && game.addOpponentRating >= opponent.CurrentRating)
            {
                throw new ArgumentOutOfRangeException("Game rating is too high");
            }

            WinStreak = 0;
            opponent.WinStreak++;
           
            
            game.addRating /= 2;
            Console.WriteLine(game.addRating);
            

            CurrentRating -= game.addRating;


            if (opponent is WinnerAc && opponent.WinStreak >= 3)
            {
                game.addOpponentRating *= 2;
            }

            opponent.CurrentRating += game.addOpponentRating;


            PrintLose(opponent, game);

            AddToStatLose(opponent, game);

            game.chooseRating(game.type);
        }
    }
}

