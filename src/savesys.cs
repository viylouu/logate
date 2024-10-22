using Newtonsoft.Json;

partial class main {
    static void save(string name, node[] n) {
        snode[] snodes = new snode[n.Length];

        for(int i = 0; i < n.Length; i++) {
            snode s = new();

            s.pos = n[i].pos;
            s.val = n[i].val;
            s.ret = nodes.IndexOf(n[i].ret);

            int[] ins = new int[n[i].ins.Length];

            for(int j = 0; j < ins.Length; j++)
                ins[j] = nodes.IndexOf(n[i].ins[j]);

            s.ins = ins;
            s.type = n[i].type;

            snodes[i] = s;
        }

        string dat = JsonConvert.SerializeObject(snodes);

        using(StreamWriter sw = new(Directory.GetCurrentDirectory() + @"\assets\saves\"+name))
            sw.Write(dat);
    }

    static List<node> load(string path) {
        snode[] s = JsonConvert.DeserializeObject<snode[]>(File.ReadAllText(path));

        node[] n = new node[s.Length];

        for(int i = 0; i < n.Length; i++) {
            n[i] = new();
            n[i].pos = s[i].pos;
            n[i].val = s[i].val;
            n[i].type = s[i].type;
            n[i].ins = new node[s[i].ins.Length];
            n[i].selinrad = new float[s[i].ins.Length];
        }
        
        for(int i = 0; i < n.Length; i++) {
            if(s[i].ret != -1)
                n[i].ret = n[s[i].ret];
            for(int j = 0; j < n[i].ins.Length; j++)
                if(s[i].ins[j] != -1)
                    n[i].ins[j] = n[s[i].ins[j]];
        }

        return new List<node>(n);
    }
}