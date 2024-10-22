public class node { 
    public Vector2 pos { get; set; }
    public gate type { get; set; }
    public node[] ins { get; set; }
    public node ret { get; set; }
    public bool val { get; set; }

    //non save specific stuff

    public float selcircrad { get; set; }
    public float[] selinrad { get; set; }
    public float seloutrad { get; set; }
    public Vector2 Dpos { get; set; }
    public bool drag { get; set; }
}