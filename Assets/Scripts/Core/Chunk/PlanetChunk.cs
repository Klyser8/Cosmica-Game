using System;
using Cosmica.Planet;
using UnityEngine;

namespace Cosmica.Core.Chunk
{
    public class PlanetChunk
    {
        
        private GameObject _chunkGameObject;
        private ChunkRenderer _chunkRenderer;
        public ChunkPosition ChunkPosition { get; }
        public int[,,] ChunkVoxels { get; } = new int[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];

        private AssetRegistry _assetRegistry;
        private PlanetHandler _planetHandler;
        private PlanetGenerator _planetGenerator;
        
        public PlanetChunk(PlanetHandler planetHandler, int x, int y, int z)
        {
            _assetRegistry = AssetRegistry.Instance;
            _planetHandler = planetHandler;
            _planetGenerator = _planetHandler.PlanetGenerator;
            ChunkPosition = new ChunkPosition(x, y, z);
            
            PopulateChunkVoxels();
            
            _chunkGameObject = new GameObject();
            _chunkGameObject.transform.position = new Vector3(
                ChunkPosition.X * ChunkRenderer.ChunkWidth, 
                ChunkPosition.Y * ChunkRenderer.ChunkHeight, 
                ChunkPosition.Z * ChunkRenderer.ChunkWidth);
            _chunkGameObject.name = "Chunk (" + ChunkPosition.X + ", " + ChunkPosition.Y + ", " + ChunkPosition.Z + ")";
            _chunkGameObject.transform.SetParent(_planetGenerator.ChunkHolder.transform);
            
            _chunkRenderer = new ChunkRenderer(_chunkGameObject, this);
        }

        /// <summary>
        /// This method populates the ChunkVoxels array, with the ID of each voxel.
        /// It iterates through each voxel in the chunk and assigns it a default value.
        /// </summary>
        private void PopulateChunkVoxels()
        {
            for (int y = 0; y < ChunkRenderer.ChunkHeight; y++)
            {
                for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
                {
                    for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
                    {
                        ChunkVoxels[x, y, z] = _planetGenerator.PickVoxel(ChunkToWorldPosition(new Vector3(x, y, z)));
                    }
                }
            }
        }
        
        /// <summary>
        /// Converts the given x position in the chunk to a position in the world.
        /// </summary>
        /// <param name="x">The x position in the chunk.</param>
        /// <returns>The x position in the world.</returns>
        public float ChunkXToWorldX(float x)
        {
            return ChunkPosition.X * ChunkRenderer.ChunkWidth + x;
        }
        
        /// <summary>
        /// Converts the given y position in the chunk to a position in the world.
        /// </summary>
        /// <param name="y">The y position in the chunk.</param>
        /// <returns>The y position in the world.</returns>
        public float ChunkYToWorldY(float y)
        {
            return ChunkPosition.Y * ChunkRenderer.ChunkHeight + y;
        }
        
        /// <summary>
        /// Converts the given z position in the chunk to a position in the world.
        /// </summary>
        /// <param name="z">The z position in the chunk.</param>
        /// <returns>The z position in the world.</returns>
        public float ChunkZToWorldZ(float z)
        {
            return ChunkPosition.Z * ChunkRenderer.ChunkWidth + z;
        }

        /// <summary>
        /// This method converts a position in the chunk to a position in the world.
        /// </summary>
        /// <param name="position">The position inside the chunk.</param>
        /// <returns></returns>
        public Vector3 ChunkToWorldPosition(Vector3 position)
        {
            return new Vector3(ChunkXToWorldX(position.x), ChunkYToWorldY(position.y), ChunkZToWorldZ(position.z));
        }
        
    }
}
