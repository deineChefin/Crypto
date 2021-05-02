public struct RSAKey
{
    public RSAKey(int e, int d, int N){
        privateKey = d; 
        ePubKey = e; 
        NPubKey = N; 
    }

    public int privateKey {get; }
    public int ePubKey {get; }
    public int NPubKey {get; }
}