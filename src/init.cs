partial class main {
    static void init() {
        Window.Title = "logate";

        //Simulation.SetFixedResolution(640, 360, Color.Black, false, false, false);

        loadtexs();
        loadaudio();

        ballshad = new();

        cam = Vector2.Zero;
    }

    static void loadaudio() {
        musicsfx = Audio.LoadSound(@"assets\audio\main theme.wav");
        placesfx = Audio.LoadSound(@"assets\audio\placenode.wav");
        grabsfx = Audio.LoadSound(@"assets\audio\grabnode.wav");
        dropsfx = Audio.LoadSound(@"assets\audio\dropnode.wav");
        delsfx = Audio.LoadSound(@"assets\audio\delnode.wav");
        calcsfx = Audio.LoadSound(@"assets\audio\calcval.wav");

        //musicpb = musicsfx.Loop();
    }

    static void loadtexs() {
        gatetex = Graphics.LoadTexture(@"assets\sprites\gates.png");
    }
}