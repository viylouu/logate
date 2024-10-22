partial class main {
    static bool caninteract;
    static Vector2 worldmouse;
    static bool inbounds;
    static Vector2 sspos;
    static bool moving;

    static void nodestuff(ICanvas c) {
        worldmouse = (Mouse.Position-center)*Dzoom+Dcam;

        if(Mouse.IsButtonReleased(MouseButton.Left))
            moving = false;

        for(int n = 0; n < nodes.Count; n++) {
            //-------------------------- variables ------------------------------------
            caninteract = worldmouse.X > nodes[n].Dpos.X - 24 && 
                          worldmouse.Y > nodes[n].Dpos.Y - 24 &&
                          worldmouse.X < nodes[n].Dpos.X + 24 && 
                          worldmouse.Y < nodes[n].Dpos.Y + 24;

            sspos = (nodes[n].Dpos-Dcam)*zoom1d+center;

            inbounds = sspos.X > -32*zoom1d && 
                       sspos.Y > -32*zoom1d && 
                       sspos.X < Window.Width+32*zoom1d && 
                       sspos.Y < Window.Height+32*zoom1d;

            //-------------------------- draw node texture ------------------------------------
            if(inbounds) {
                c.DrawTexture(
                    gatetex, 
                    new Rectangle(
                        ((int)nodes[n].type*2+(nodes[n].val==false?0:1))%5*32, 
                        math.floor(((int)nodes[n].type*2+(nodes[n].val==false?0:1))/5)*32,
                        32,32
                    ),
                    new Rectangle(
                        math.round(sspos), 
                        Vector2.One*32*zoom1d, 
                        Alignment.Center
                    )
                );
            }

            //-------------------------- moving and deletion ------------------------------------
            if(inbounds && caninteract && math.sqrdist(worldmouse, nodes[n].pos) <= math.sqr(18)) {
                nodes[n].selcircrad += (1-nodes[n].selcircrad)/(6/(Time.DeltaTime*60));

                if(Mouse.IsButtonPressed(MouseButton.Left) && !dragging) {
                    nodes[n].drag = true;
                    if(playgrabsfx) { grabsfx.Play(); playgrabsfx = false; }
                    moving = true;
                    dragging = true;
                }

                if(Mouse.IsButtonPressed(MouseButton.Right)) {
                    for(int i = 0; i < nodes[n].ins.Length; i++)
                        if(nodes[n].ins[i] != null)
                            nodes[n].ins[i].ret = null;

                    if(nodes[n].ret != null)
                        for(int i = 0; i < nodes[n].ret.ins.Length; i++)
                            if(nodes[n].ret.ins[i] == nodes[n])
                                nodes[n].ret.ins[i] = null;

                    nodes.RemoveAt(n);
                    n--;
                    if(playdelsfx) { delsfx.Play(); playdelsfx = false; }
                    continue;
                }

                if(Keyboard.IsKeyPressed(Key.Space) && nodes[n].type == gate.val) {
                    nodes[n].val = !nodes[n].val;
                    if(playcalcsfx) {
                        calcsfx.Play();
                        playcalcsfx = false;
                    }
                }
            }
            else
                nodes[n].selcircrad += (-nodes[n].selcircrad)/(6/(Time.DeltaTime*60));

            //-------------------------- node moving ------------------------------------
            if(nodes[n].drag) {
                nodes[n].pos = worldmouse;

                if(Keyboard.IsKeyDown(Key.LeftControl))
                    nodes[n].pos = math.round(worldmouse/8)*8;
            }

            //-------------------------- stop moving node ------------------------------------
            if(Mouse.IsButtonReleased(MouseButton.Left) && nodes[n].drag) {
                nodes[n].drag = false;
                if(playdropsfx) { dropsfx.Play(); playdropsfx = false; }
                dragging = false;
            }

            //-------------------------- draw the selection highlight circle ------------------------------------
            if(inbounds && nodes[n].selcircrad >= 0.1f) {
                c.Stroke(new Color(108, 214, 89));
                c.StrokeWidth(2*zoom1d*nodes[n].selcircrad);

                c.DrawCircle(sspos, 18*zoom1d*nodes[n].selcircrad);
            }

            //-------------------------- wiring points ------------------------------------

            //-------------------------- inputs ------------------------------------
            for(int i = 0; i < nodes[n].ins.Length; i++) {
                Vector2 worldpos = nodes[n].pos+new Vector2(-20,i*12-nodes[n].ins.Length/2*12+(nodes[n].ins.Length%2==0?6:0));
                Vector2 circpos = sspos+new Vector2(-20,i*12-nodes[n].ins.Length/2*12+(nodes[n].ins.Length%2==0?6:0))*zoom1d;

                if(inbounds && zoom1d >= 0.125f) {
                    c.Fill(new Color(227, 186, 141));
                    c.DrawCircle(circpos, 2*zoom1d);
                }

                if(inbounds && caninteract && math.sqrdist(worldmouse,worldpos) <= 12.25f) {
                    nodes[n].selinrad[i] += (1-nodes[n].selinrad[i])/(6/(Time.DeltaTime*60));

                    if(!wiring && !moving && Mouse.IsButtonPressed(MouseButton.Left)) {
                        wiring = true;
                        nodew = n;
                        nodeio = (byte)(i+1);
                    }

                    if(wiring && Mouse.IsButtonReleased(MouseButton.Left)) {
                        wiring = false;

                        if(nodeio == 0) {
                            nodes[nodew].ret = nodes[n];
                            nodes[n].ins[i] = nodes[nodew];
                        }
                    }
                }
                else
                    nodes[n].selinrad[i] += (-nodes[n].selinrad[i])/(6/(Time.DeltaTime*60));

                if(inbounds && nodes[n].selinrad[i] >= 0.3f) {
                    c.Stroke(new Color(108, 214, 89));
                    c.StrokeWidth(1*zoom1d*nodes[n].selinrad[i]);

                    c.DrawCircle(circpos,3.5f*zoom1d*nodes[n].selinrad[i]);
                }
            }

            //-------------------------- output ------------------------------------
            if(nodes[n].type != gate.ret) { 
                Vector2 worldpos = new Vector2(20,0)+nodes[n].pos;
                Vector2 circpos = sspos+new Vector2(20*zoom1d,0);

                if(inbounds && zoom1d >= 0.125f) {
                    c.Fill(new Color(227, 186, 141));
                    c.DrawCircle(circpos, 2*zoom1d);
                }

                if(inbounds && caninteract && math.sqrdist(worldmouse,worldpos) <= 12.25f) {
                    nodes[n].seloutrad += (1-nodes[n].seloutrad)/(6/(Time.DeltaTime*60));

                    if(!wiring && !moving && Mouse.IsButtonPressed(MouseButton.Left)) {
                        wiring = true;
                        nodew = n;
                        nodeio = 0;
                    }

                    if(wiring && Mouse.IsButtonReleased(MouseButton.Left)) {
                        wiring = false;

                        if(nodeio != 0) {
                            nodes[nodew].ins[nodeio-1] = nodes[n];
                            nodes[n].ret = nodes[nodew];
                        }
                    }
                } else
                    nodes[n].seloutrad += (-nodes[n].seloutrad) /(6/(Time.DeltaTime*60));

                if(inbounds && nodes[n].seloutrad >= 0.3f) {
                    c.Stroke(new Color(108, 214, 89));
                    c.StrokeWidth(1*zoom1d*nodes[n].seloutrad);

                    c.DrawCircle(circpos,3.5f*zoom1d*nodes[n].seloutrad);
                }
            }
             
            //move the display pos to the actual pos
            nodes[n].Dpos += (nodes[n].pos-nodes[n].Dpos)/(3/(Time.DeltaTime*60));

            //-------------------------- logic ------------------------------------

            bool prevval = nodes[n].val;

            switch(nodes[n].type) {
                case gate.buf:
                case gate.ret:
                    if(nodes[n].ins[0] != null)
                        nodes[n].val = nodes[n].ins[0].val;
                    break;
                case gate.not:
                    if(nodes[n].ins[0] != null)
                        nodes[n].val = !nodes[n].ins[0].val; 
                    break;
                case gate.and:
                    if(nodes[n].ins[0] != null && nodes[n].ins[1] != null)
                        nodes[n].val = nodes[n].ins[0].val && nodes[n].ins[1].val;
                    break;
                case gate.nand:
                    if(nodes[n].ins[0] != null && nodes[n].ins[1] != null)
                        nodes[n].val = !(nodes[n].ins[0].val && nodes[n].ins[1].val);
                    break;
                case gate.or:
                    if(nodes[n].ins[0] != null && nodes[n].ins[1] != null)
                        nodes[n].val = nodes[n].ins[0].val || nodes[n].ins[1].val;
                    break;
                case gate.nor:
                    if(nodes[n].ins[0] != null && nodes[n].ins[1] != null)
                        nodes[n].val = !(nodes[n].ins[0].val || nodes[n].ins[1].val);
                    break;
                case gate.xor:
                    if(nodes[n].ins[0] != null && nodes[n].ins[1] != null)
                        nodes[n].val = nodes[n].ins[0].val ^ nodes[n].ins[1].val;
                    break;
                case gate.xnor:
                    if(nodes[n].ins[0] != null && nodes[n].ins[1] != null)
                        nodes[n].val = !(nodes[n].ins[0].val ^ nodes[n].ins[1].val);
                    break;
            }

            if(playcalcsfx && nodes[n].val != prevval) {
                calcsfx.Play();
                playcalcsfx = false;
            }
        }

        if(Mouse.IsButtonReleased(MouseButton.Left))
            wiring = false;
    }
}