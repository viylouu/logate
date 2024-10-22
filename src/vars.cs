partial class main {
    //textures
    static ITexture gatetex;

    //sound
    static ISound musicsfx;
    static SoundPlayback musicpb;
    static ISound placesfx;
    static ISound grabsfx;
    static ISound dropsfx;
    static ISound delsfx;
    static ISound calcsfx;

    static bool playdropsfx, playgrabsfx, playdelsfx, playcalcsfx;

    //misc
    static List<node> nodes = new List<node>();
    static bshad ballshad;
    static bool dragging;

    static float tickrate = 1f/30;
    static float tick = 0;

    static bool upd;

    //camera
    static Vector2 cam;
    static float zoom = 1;

    static Vector2 Dcam;
    static float Dzoom = 1;
    static float zoom1d = 1;

    static Vector2 center;

    //wiring
    static bool wiring;
    static int nodew;
    static byte nodeio;

    //save and load
    static int selfile;
    static string sfname = "";

    static string[] saves;
    static string[] savenames;
}