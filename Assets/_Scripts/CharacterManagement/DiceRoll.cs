using System;

[Serializable]
public enum Dice
{
    d2, d4, d6, d8, d10, d12, d20
}
[Serializable]
public class DiceRoll
{
    public Dice dice;
    public int numberOfDice;
    public override string ToString()
    {
        return numberOfDice.ToString() + dice.ToString();
    }
    public DiceRoll()
    {

    }
    public DiceRoll(Dice dice, int number)
    {
        this.dice = dice;
        this.numberOfDice = number;
    }
    public static DiceRoll OneD6() {
        return new DiceRoll(Dice.d6, 1);
        
    }
}
