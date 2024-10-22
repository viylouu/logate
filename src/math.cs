public class math { 
    public static float floor(float a) => MathF.Floor(a);
    public static Vector2 round(Vector2 a) => new(MathF.Round(a.X),MathF.Round(a.Y));
    public static float sqr(float a) => a * a;
    public static float sqrdist(Vector2 a, Vector2 b) => sqr(b.X-a.X) + sqr(b.Y-a.Y);
}