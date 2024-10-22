using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;
using SimulationFramework.Drawing.Shaders;

public class bshad : CanvasShader {
    public int width, height;
    public float time;
    public Vector2 mousePosition;

    public override ColorF GetPixelColor(Vector2 position) {
        Vector2 size = MakeVector2(width, height);
        Vector3 c = default;
        float l = 0, z = time;
        for(int i = 0; i < 3; i++) {
            Vector2 uv, p = position / size;
            uv = p;
            p -= mousePosition / size;
            p.X *= size.X / size.Y;
            z += .07f;
            l = Length(p);
            uv += p / l * (Sin(z) + 1.0f) * Abs(Sin(l * 9.0f - z - z));
            c[i] = .01f / Length(Mod(uv, 1.0f) - MakeVector2(.5f));
        }
        return new ColorF(MakeVector4(c*(1f/l),time));
    }
}