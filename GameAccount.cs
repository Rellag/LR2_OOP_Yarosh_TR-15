using System;
namespace LR2
{
    public class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating { get;  set; }
        public int GamesCount { get; set; }
        public int WinStreak { get;  set; }
        
        // types: common, safeLooser, winStreaker
        public GameAccount(string userName, int сurrentRating, int gamesCount)
        {
            if (сurrentRating < 0 || gamesCount < 0)
            {
                throw new ArgumentOutOfRangeException("Negative values");
            }

            UserName = userName;
            CurrentRating = сurrentRating;
            GamesCount = gamesCount;
        }


        public virtual void WinGame(GameAccount opponent, Game game)
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
            if(WinStreak >= 3)//if 3 games were won 
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

        public virtual void LoseGame(GameAccount opponent, Game game)
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
            
         
            CurrentRating -= game.addRating;
            

            if (opponent.WinStreak >= 3 && opponent is WinnerAc)
            {
                game.addOpponentRating *= 2;
            }
           
            opponent.CurrentRating += game.addOpponentRating;
            
           
            PrintLose(opponent, game);

            AddToStatLose(opponent, game);

            game.chooseRating(game.type);

        }

        public void AddToStatLose(GameAccount opponent, Game game)
        {
            Stat.ManeStat.AddFirst(new Stat( game.addOpponentRating, game.addRating, opponent.UserName, UserName, opponent.CurrentRating, CurrentRating));

            Stat.seedIndex++;

        }

        public void AddToStatWin(GameAccount opponent, Game game)
        {
            Stat.ManeStat.AddFirst(new Stat(game.addRating,game.addOpponentRating, UserName, opponent.UserName, CurrentRating, opponent.CurrentRating));

            Stat.seedIndex++;

        }

        public void PrintLose(GameAccount opponent, Game game)
        {
            Console.WriteLine(UserName + " " + CurrentRating + " (- {0}) " + "LOST" + " " +
                opponent.UserName + " " + opponent.CurrentRating + " (+ {1})", game.addRating, game.addOpponentRating);
        }

        public void PrintWin(GameAccount opponent, Game game)
        {
            Console.WriteLine(UserName + " " + CurrentRating + " (+ {0}) " + "WON" + " " +
                 opponent.UserName + " " + opponent.CurrentRating + " (- {1})", game.addRating, game.addOpponentRating);
        }

       


       
    }

    public class Game
    {
        public int addRating { get; set; }
        public int addOpponentRating { get; set; }
        public string type { get; set; }

        public Game(string type)
        {
            this.type = type;
            chooseRating(type);
        }

        public void chooseRating(string type)
        {
            switch (type)
            {
                case "standart":
                    addRating = 25;
                    addOpponentRating = 25;
                    break;
                case "traning":
                    addRating = 0;
                    addOpponentRating = 0;
                    break;
                case "onlyYourRating":
                    addRating = 25;
                    addOpponentRating = 0;
                    break;
                case "onlyOpponentRating":
                    addRating = 0;
                    addOpponentRating = 25;
                    break;
                default:
                    Console.WriteLine("Unkown type. Canged to standart type");
                    addRating = 25;
                    addOpponentRating = 25;
                    break;

            }

        }
    }

    public class GetGame
    {
        public Game GetStandartGame()
        {
            return new Game("standart");
        }

        public Game GetTrainingGame()
        {
            return new Game("traning");
        }

        public Game GetYouRatingGame()
        {
            return new Game("onlyYourRating");
        }

        public Game GetOppRatingGame()
        {
            return new Game("onlyOpponentRating");
        }
    }


    public class Stat
    {
        public int addWinner { get; set; }
        public int addLooser { get; set; }
        public int index { get; }
        public string winner { get; set; }
        public string looser { get; set; }
        public int winnerRating { get; }
        public int looserRating { get; }
        public static int seedIndex = 42000;

        public Stat(int addRating, int addOpponentRating, string winner, string looser, int winnerRating, int looserRating)
        {
            this.addWinner = addRating;
            this.addLooser = addOpponentRating;
            this.winner = winner;
            this.looser = looser;
            this.winnerRating = winnerRating;
            this.looserRating = looserRating;
            this.index = seedIndex;
        }

        public static LinkedList<Stat> ManeStat = new LinkedList<Stat> { };

        public static void GetStats()
        {

            Console.WriteLine("\nStats:");
            foreach (var item in ManeStat)
            {
                Console.WriteLine("Game index: {0}, Winner: {1} {2} (+ {3})," +
                    " Looser: {4} {5}(- {6})", item.index, item.winner, item.winnerRating
                    ,item.addWinner, item.looser, item.looserRating, item.addLooser);
            }

        }

        public static void GetStats(GameAccount gameAccount)
        {
            Console.WriteLine("\nStats of {0}:", gameAccount.UserName);
            foreach (var item in ManeStat)
            {
                if (gameAccount.UserName.Equals(item.winner) || gameAccount.UserName.Equals(item.looser))
                {
                    Console.WriteLine("Game index: {0}, Winner: {1} {2} (+ {3})," +
                        " Looser: {4} {5}(- {6})", item.index, item.winner, item.winnerRating,
                       item.addWinner, item.looser, item.looserRating, item.addLooser);
                }
            }
        }
    }
}

