public struct RSAKey
{
    public RSAKey(int e, int d, int N){
        this.PrivateKey = d; 
        this.EPubKey = e; 
        this.NPubKey = N; 
    }

    public int PrivateKey {get; }
    public int EPubKey {get; }
    public int NPubKey {get; }

    public void print(){
        System.Console.WriteLine("private key (d): {0}", this.PrivateKey);
        System.Console.WriteLine("public key (e, N): {0}, {1}", this.EPubKey, this.NPubKey);
    }
}