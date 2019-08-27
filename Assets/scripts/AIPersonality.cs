public class AIPersonality
{
    private float aggressivness;
    private float greed;

    public AIPersonality(float greed, float aggressivness) {
        this.aggressivness = aggressivness;
        this.greed = greed;
    }

    public float getAgressivness() {
        return aggressivness;
    }

    public float getGreed() {
        return greed;
    }
}
