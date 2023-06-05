namespace Cosmica.Core.Chunk
{
    public class ChunkPosition
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        
        public ChunkPosition(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}