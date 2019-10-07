using System.Collections.Generic;

public class Desires
{
    private float bloodlust;
    private float greed;
    private int frenzy;
    private Dictionary<int, int>[] hatred;

    public Desires(float greed, float bloodlust, int frenzy = 30) {
        hatred = new Dictionary<int, int> [10];
        this.bloodlust = bloodlust;
        this.greed = greed;
        this.frenzy = frenzy;
    }

    public float getBloodlust() {
        return bloodlust;
    }

    public float getGreed() {
        return greed;
    }

    public int getFrenzyPoint()
    {
        return frenzy;
    }

    public void addPlayerToHatred(int PlayerNumber)
    {
        hatred.SetValue(PlayerNumber, 0);
    }

    public void increasePlayerHatred(int playerNumber, int amount)
    {
        hatred.SetValue(playerNumber, (int)hatred.GetValue(playerNumber) + amount);
    }
}
