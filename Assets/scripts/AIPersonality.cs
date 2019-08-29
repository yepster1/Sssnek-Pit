public class AIPersonality
{
    private float aggressivness;
    private float greed;
    private int killPoint;

    public AIPersonality(float greed, float aggressivness, int killPoint = 30) {
        this.aggressivness = aggressivness;
        this.greed = greed;
        this.killPoint = killPoint;
    }

    public float getAgressivness() {
        return aggressivness;
    }

    public float getGreed() {
        return greed;
    }

    public int getKillPoint()
    {
        return killPoint;
    }
}
