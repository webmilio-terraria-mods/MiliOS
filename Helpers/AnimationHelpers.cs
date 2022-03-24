namespace MiliOS.Helpers;

public class AnimationHelpers
{
    public static float SmoothTranslation(float current, float destination, 
        float minSpeed = 0.00005f, float a = 0.05f)
    {
        float y = a - minSpeed;
        float delta = y * (destination - current);

        if (delta < 0)
            delta -= minSpeed;
        else
            delta += minSpeed;

        return delta;
    }
}