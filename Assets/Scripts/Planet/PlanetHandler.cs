using Cosmica.Core;
using Cosmica.Core.Block;
using Cosmica.Core.Chunk;
using UnityEngine;

namespace Cosmica.Planet
{
    /// <summary>
    /// Central class for the planet.
    /// While this class does not execute any logic, it holds references to all the other classes that do.
    /// </summary>
    public class PlanetHandler : MonoBehaviour
    {
        private AssetRegistry _assetRegistry;
        public PlanetSettings PlanetSettings { get; private set; }
        public PlanetGenerator PlanetGenerator { get; private set; }

        private void Awake()
        {
            _assetRegistry = AssetRegistry.Instance;
            PlanetSettings = new PlanetSettings(6, 3);
            PlanetGenerator = gameObject.AddComponent<PlanetGenerator>();
        }
        
        public PlanetChunk GetChunkAt(Vector3 globalPos)
        {
            var chunkX = Mathf.FloorToInt(globalPos.x / ChunkRenderer.ChunkWidth);
            var chunkY = Mathf.FloorToInt(globalPos.y / ChunkRenderer.ChunkHeight);
            var chunkZ = Mathf.FloorToInt(globalPos.z / ChunkRenderer.ChunkWidth);
            return PlanetGenerator.Chunks[chunkX, chunkY, chunkZ];
        }

        public BlockType GetBlockTypeAt(Vector3 globalPos)
        {
            PlanetChunk chunk = GetChunkAt(globalPos);
            var localX = Mathf.FloorToInt(globalPos.x - chunk.ChunkPosition.X * ChunkRenderer.ChunkWidth);
            var localY = Mathf.FloorToInt(globalPos.y - chunk.ChunkPosition.Y * ChunkRenderer.ChunkHeight);
            var localZ = Mathf.FloorToInt(globalPos.z - chunk.ChunkPosition.Z * ChunkRenderer.ChunkWidth);
            return _assetRegistry.BlockTypes[chunk.ChunkVoxels[localX, localY, localZ]];
        }
        
    }
}