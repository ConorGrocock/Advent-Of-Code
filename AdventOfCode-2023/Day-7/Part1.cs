using SharedUtils;

namespace Day_4;

public class Part1: ISolution
{
    private static Dictionary<char, int> cardValues = new()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 }
    };
    
    private class Hand
    {
        private string Cards;
        public HandType HandType;
        public int HighestCardValue;
        public int Bid;
        
        public Hand(string cards, int bid)
        {
            Cards = cards; 
            Bid = bid;
            
            var frequencyMap = cards.FrequencyMap();

            var orderedMap = frequencyMap.OrderByDescending(pair => pair.Value);
            var frequencyList = orderedMap.ToList();

            HighestCardValue = cardValues[frequencyList[0].Key];

            if (frequencyList[0].Value == 5)
            {
                HandType = HandType.FiveOfAKind;
            } else if (frequencyList[0].Value == 4)
            {
                HandType = HandType.FourOfAKind;
            } else if (frequencyList[0].Value == 3 && frequencyList[1].Value == 2)
            {
                HandType = HandType.FullHouse;
            } else if (frequencyList[0].Value == 3)
            {
                HandType = HandType.ThreeOfAKind;
            } else if (frequencyList[0].Value == 2 && frequencyList[1].Value == 2)
            {
                HandType = HandType.TwoPair;

                var mostCommonChars = new List<char>
                {
                    frequencyList[0].Key,
                    frequencyList[1].Key
                };

                HighestCardValue = CalculateHighestCard(mostCommonChars.ToArray());
            } else if (frequencyList[0].Value == 2)
            {
                HandType = HandType.OnePair;
            }
            else
            {
                HandType = HandType.HighCard;
                HighestCardValue = CalculateHighestCard(cards.ToCharArray());
            }
        }

        private static int CalculateHighestCard(IEnumerable<char> cards)
        {
            return cards.Select(c => cardValues[c]).OrderDescending().First();
        }

        public int GetCardIndexValue(int index)
        {
            return cardValues[Cards[index]];
        }

        public override string ToString()
        {
            return $"{Cards} - {Bid} - {HandType} - {HighestCardValue}";
        }
    }
    
    private enum HandType
    {
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1
    }
    
    public int Solve(IEnumerable<string> lines)
    {
        var hands = (
            from line in lines 
                select line.SplitAndTrim(' ') 
                into component 
                    let cards = component[0] 
                    let bid = int.Parse(component[1]) 
                    select new Hand(cards, bid)
        ).ToList();

        var orderedHands = hands
            .OrderBy(hand => hand.HandType)
            .ThenBy(hand => hand.GetCardIndexValue(0))
            .ThenBy(hand => hand.GetCardIndexValue(1))
            .ThenBy(hand => hand.GetCardIndexValue(2))
            .ThenBy(hand => hand.GetCardIndexValue(3))
            .ThenBy(hand => hand.GetCardIndexValue(4))
            .ToArray();

        return orderedHands.Select((hand, rank) => hand.Bid * (rank + 1)).Sum();
    }
    public void TestInput(int expectedOutput)
    {
        var inputLines = Input.TestInput();
        var answer = Solve(inputLines);

        Console.Write($"{answer} - {expectedOutput} = {(answer == expectedOutput ? "Correct" : "Incorrect")}");
    }

    public void RealInput()
    {
        var inputLines = Input.ReadAsLines();
        var answer = Solve(inputLines);

        Console.Write($"{answer}");
    }
}