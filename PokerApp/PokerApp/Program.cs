using PokerApp.BusinessLogic;
using PokerApp.Model;
using PokerApp.Utils;

namespace PokerApp;

class Program
{
    static void Main(string[] args)
    {
        //logica di inserimento nomi, numero giocatori, (chips iniziali, small and big blind -> valore default)
        Dealer dealer = new("Marco", "Pietro", "Gianni", "Egidio");

        dealer.GiveHands();
        
        foreach (var player in dealer.Players)
        {
            Console.WriteLine(player.Name);
            Console.WriteLine(player.Hand[0]);
            Console.WriteLine(player.Hand[1]);
        }
        dealer.AddFlopTurnAndRiver();

        Console.WriteLine("*********************************************");
        foreach (Card c in dealer.Board)
        {
            Console.WriteLine(c);
        }
        
        Console.WriteLine($"Deck size after dealing: {dealer.Deck.Cards.Count}");

        Console.WriteLine("\n\nAdesso controlliamo i punteggi: ");
        dealer.StartScoreChecking();
        
        foreach (var pl in dealer.Players)
        {
            Console.WriteLine(pl.Name + "   " + pl.Score);
        }

        Console.WriteLine("stampiamo gli actualpLAYERS");
        foreach (var p in dealer.LastRoundPlayers)
        {
            Console.WriteLine("ihihi" + p.Name);
        }
        
        
        var winners = dealer.BreakTiesAndPickWinner();
        
        Console.WriteLine("\n\nrimangono in gioco vincitor* :");
        foreach (var winner in winners)
        {
            Console.WriteLine(winner.Name + "  punteggio: " + winner.Score);
        }


    }
}