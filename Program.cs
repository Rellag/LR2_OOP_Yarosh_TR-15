// See https://aka.ms/new-console-template for more information
using LR2;

Console.WriteLine("Hello, World!");

var Vlad = new GameAccount("vlad", 1000, 0, "safeLooser");
var Ilysha = new GameAccount("ilysha", 1000, 0);
var Alex = new GameAccount("alex", 1000, 0);
var Bodya = new GameAccount("bodya", 1000, 0, "winStreaker");

var game = new GetGame();


Vlad.WinGame(Ilysha, game.GetStandartGame());
Vlad.LoseGame(Ilysha, game.GetTrainingGame());

Ilysha.WinGame(Vlad, game.GetOppRatingGame());

Alex.WinGame(Vlad, game.GetOppRatingGame());

Alex.LoseGame(Vlad, new Game("asd"));

Bodya.WinGame(Vlad, game.GetStandartGame());
Bodya.WinGame(Alex, game.GetStandartGame());
Ilysha.LoseGame(Bodya, game.GetStandartGame());



Stat.GetStats();

Stat.GetStats(Vlad);