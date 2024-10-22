using Newtonsoft.Json;

partial class main {
    static void save(string name, node[] n) {
        snode[] snodes = new snode[n.Length];;

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
        snode[] s = JsonConvert.DeserializeObject<snode[]>(path);

        List<node> n = new List<node>();

        for(int i = 0; i < s.Length; i++) {
            node _n = new();

            _n.pos = s[i].pos;
            _n.val = s[i].val;
            _n.type = s[i].type;
            _n.ins = new node[s[i].ins.Length];

            n.Add(_n);
        }

        for(int i = 0; i < s.Length; i++) {
            n[i].ret = n[s[i].ret];
            for(int j = 0; j < n[i].ins.Length; j++)
                n[i].ins[j] = n[s[i].ins[j]];
        }

        return n;
    }
}