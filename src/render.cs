partial class main { 
    static void rend(ICanvas c) {
        c.Clear(new Color(26,18,11));

        center = new(Window.Width/2,Window.Height/2);

        resetsoundbools();

        nodestuff(c);

        c.Stroke(new Color(47, 122, 118));
        c.StrokeWidth(2);

        //wire drawing
        for(int n = 0; n < nodes.Count; n++) {
            if(nodes[n].ins.Length == 0)
                continue;

            for(int i = 0; i < nodes[n].ins.Length; i++) {
                if(nodes[n].ins[i] == null)
                    continue;

                Vector2 inpos = ((nodes[n].Dpos-Dcam)*zoom1d)+new Vector2(-20,i*12-nodes[n].ins.Length/2*12+(nodes[n].ins.Length%2==0?6:0))*zoom1d+center;

                c.DrawLine(inpos,(nodes[n].ins[i].pos-Dcam)*zoom1d+new Vector2(20,0)*zoom1d+center);
            }
        }

        if(wiring)
            c.DrawLine((nodes[nodew].Dpos-Dcam)*zoom1d+center+zoom1d*new Vector2(nodeio==0?20:-20,nodeio==0?0:((nodeio-1)*12-nodes[nodew].ins.Length/2*12+(nodes[nodew].ins.Length%2==0?6:0))), Mouse.Position);

        //ball of life
        ballshad.time = Time.TotalTime;
        ballshad.mousePosition = -Dcam*zoom1d+center;
        ballshad.width = (int)(48*zoom1d);
        ballshad.height = (int)(48*zoom1d);

        c.Fill(ballshad);
        c.DrawCircle(-Dcam*zoom1d+center, 24*zoom1d);

        controls();

        ui();
    }

    static void resetsoundbools() { 
        playgrabsfx = true;
        playdropsfx = true;
        playdelsfx = true;
        playcalcsfx = true;
    }

    static void ui() { 
        ImGui.Begin("controls");

        var ww = ImGui.GetWindowWidth();
        var tw = ImGui.CalcTextSize("--- nodes ---").X;

        ImGui.SetCursorPosX((ww-tw)*.5f);
        ImGui.Text("--- nodes ---");

        ImGui.Text("- buffer (1)");
        ImGui.Text("- and (2)");
        ImGui.Text("- or (3)");
        ImGui.Text("- xor (4)");
        ImGui.Text("- input (5)");
        ImGui.Text("- not (ctrl + 1)");
        ImGui.Text("- nand (ctrl + 2)");
        ImGui.Text("- nor (ctrl + 3)");
        ImGui.Text("- xnor (ctrl + 4)");
        ImGui.Text("- output (ctrl + 5)");
        ImGui.NewLine();

        tw = ImGui.CalcTextSize("--- camera ---").X;
        ImGui.SetCursorPosX((ww - tw) * .5f);
        ImGui.Text("--- camera ---");

        ImGui.Text("- move (mmb + move mouse)");
        ImGui.Text("- zoom (scroll)");
        ImGui.Text("- reset cam (0)");

        ImGui.End();

        ImGui.Begin("debug");

        ImGui.Text($"{(1/Time.DeltaTime).ToString("0000.000")} fps");
        ImGui.Text($"{nodes.Count()} node" + (nodes.Count()==1?"":"s"));

        ImGui.End();
    }
}