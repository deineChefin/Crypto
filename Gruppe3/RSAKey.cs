using System.Numerics;

namespace Gruppe3 { 
    public struct RSAKey
    {
        public RSAKey(BigInteger e, BigInteger d, BigInteger N)
        {
            this.PrivateKey = d;
            this.EPubKey = e;
            this.NPubKey = N;
        }

        public BigInteger PrivateKey { get; }
        public BigInteger EPubKey { get; }
        public BigInteger NPubKey { get; }

        public override string ToString()
        {
            return $"private key (d): {this.PrivateKey}, public key (e): {this.EPubKey}, Modulus (N): {this.NPubKey}";
        }
    }
}