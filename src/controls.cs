partial class main {
    static void controls() { 
        if(!Keyboard.IsKeyDown(Key.LeftControl)) {
            if(Keyboard.IsKeyPressed(Key.Key1))
                create(gate.buf, 1);
            if(Keyboard.IsKeyPressed(Key.Key2))
                create(gate.and, 2);
            if(Keyboard.IsKeyPressed(Key.Key3))
                create(gate.or, 2);
            if(Keyboard.IsKeyPressed(Key.Key4))
                create(gate.xor, 2);
            if(Keyboard.IsKeyPressed(Key.Key5))
                create(gate.val, 0);
        } else {
            if(Keyboard.IsKeyPressed(Key.Key1))
                create(gate.not, 1);
            if(Keyboard.IsKeyPressed(Key.Key2))
                create(gate.nand, 2);
            if(Keyboard.IsKeyPressed(Key.Key3))
                create(gate.nor, 2);
            if(Keyboard.IsKeyPressed(Key.Key4))
                create(gate.xnor, 2);
            if(Keyboard.IsKeyPressed(Key.Key5))
                create(gate.ret, 1);
        }

        float pzoom = zoom;
        zoom -= Mouse.ScrollWheelDelta * (zoom*.125f);
        Dzoom += (zoom-Dzoom) / (6/(Time.DeltaTime*60));
        zoom1d = 1/Dzoom;

        Dcam += (cam-Dcam) / (6/(Time.DeltaTime*60));

        if(Mouse.IsButtonDown(MouseButton.Middle))
            cam -= Mouse.DeltaPosition * Dzoom;

        if(Keyboard.IsKeyPressed(Key.Key0)) {
            cam = Vector2.Zero;
            zoom = 1;

            if(math.sqrdist(cam, Dcam) >= math.sqr(4000))
                Dcam = cam;
        }
    }

    static void create(gate t, byte ins) {
        nodes.Add(
            new() { 
                pos = (Mouse.Position-center)*Dzoom+cam,
                type = t,
                val = false,
                ins = new node[ins],
                selinrad = new float[ins]
            }
        );

        placesfx.Play();
    }
}