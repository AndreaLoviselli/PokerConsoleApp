using PokerApp.Rules;

namespace PokerApp.Model;

public class Player
{
    public string Name { get; set; }
    public List<Card> Hand { get; set; }
    public List<Card> HandPlusBoardDesc { get; set; }
    public float Score { get; set; }
    public int Chips { get; set; }

    public Player(string name)
    {
        this.Name = name;
        this.Hand = new();
        this.HandPlusBoardDesc = new();
        this.Score = 0;
        this.Chips = Manual.StartingChips;
    }

    public void AddBoardToHandDesc(Card[] board)
    {
        this.HandPlusBoardDesc.AddRange(this.Hand);
        this.HandPlusBoardDesc.AddRange(board);
        this.HandPlusBoardDesc =
            this.HandPlusBoardDesc
                .OrderByDescending(c => c.Number)
                .ToList();
    }
}