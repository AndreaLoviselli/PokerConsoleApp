
using PokerApp.Model;
using PokerApp.Rules;
using PokerApp.Utils;

namespace PokerApp.BusinessLogic;

public class Dealer //o Table?
{
    public Deck Deck { get; set; }
    public Card[] Board { get; }
    public List<Player> Players { get; }
    //public List<Player> Classification { get; set; }
    public List<Player> LastRoundPlayers { get; set; }

    // SI CREA DEALER, MAZZO, GIOCATORI E SI DANNO LE CARTE
    public Dealer( params string[] playerNames)
    {
        this.Deck = new();
        this.Board = new Card[Manual.BoardSize];
        this.Players = new();
        this.LastRoundPlayers = new();
        
        foreach (var name in playerNames)
            this.Players.Add(new Player(name));
        
    }
    public void GiveHands()
    {
        for (int i = 0; i < Manual.DealingRound; i++)
        {
            foreach (Player pl in this.Players)
                pl.Hand.Add(this.Deck.Cards.Pop());
        }

        InizializeLastRoundPlayers();
    }

    private void InizializeLastRoundPlayers()
    {
        this.LastRoundPlayers = this.Players;
    }
    public void AddFlopTurnAndRiver()
    {
        Discard();

        for (int i = 0; i < Manual.BoardSize; i++)
        {
            if (i == Manual.TurnDiscard) Discard();
            if (i == Manual.RiverDiscard) Discard();
            this.Board[i] = this.Deck.Cards.Pop();
        } 
    }
    private void Discard()
    {
        this.Deck.Cards.Pop();
    }
    
    // SI CONTROLLA IL VINCITORE TRA I GIOCATORI CHE NON HANNO FOLDATO
    public void StartScoreChecking()
    {
        foreach (Player pl in this.LastRoundPlayers)
        {
            pl.AddBoardToHandDesc(this.Board);
            pl.Score = ScoreChecker.Check(pl.HandPlusBoardDesc);
        }
        
    }

    public List<Player> BreakTiesAndPickWinner()
    {
        var maxScore = this.LastRoundPlayers.Max(p => p.Score);
        
        var maxScorePlayers = this.LastRoundPlayers
            .OrderByDescending(p => p.Score)
            .TakeWhile(p => p.Score == maxScore)
            .ToList();

        Console.WriteLine(this.LastRoundPlayers.First().Name);
        
        if (maxScorePlayers.Count > 1) 
            return TiesBreaker.BreakTie(maxScorePlayers);

        return new List<Player> { maxScorePlayers.First() };
    }
        
    }
    
