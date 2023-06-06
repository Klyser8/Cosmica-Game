using UnityEngine;

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
        
        /// <summary>
        /// Converts the chunk position to a Vector3.
        /// </summary>
        /// <returns>The chunk position as a Vector3.</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
        
        /// <summary>
        /// Converts the chunk position to a world position (Vector3).
        /// </summary>
        /// <returns>The chunk position as a world position (Vector3).</returns>
        public Vector3 ToWorldPosition()
        {
            return new Vector3(X * ChunkRenderer.ChunkWidth, Y * ChunkRenderer.ChunkHeight, Z * ChunkRenderer.ChunkWidth);
        }
    }
}