using System;
using Cosmica.Planet;
using UnityEngine;

namespace Cosmica.Core.Chunk
{
    public class PlanetChunk
    {
        public GameObject ChunkGameObject { get; }

        private ChunkRenderer _chunkRenderer;
        public ChunkPosition ChunkPosition { get; }
        public int[,,] ChunkVoxels { get; } = new int[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];

        private PlanetHandler _planetHandler;
        private PlanetGenerator _planetGenerator;
        public PlanetChunk(PlanetHandler planetHandler, int x, int y, int z)
        {
            _planetHandler = planetHandler;
            _planetGenerator = planetHandler.PlanetGenerator;
            ChunkPosition = new ChunkPosition(x, y, z);
            
            PopulateChunkVoxels();
            
            ChunkGameObject = new GameObject();
            ChunkGameObject.transform.position = new Vector3(
                ChunkPosition.X * ChunkRenderer.ChunkWidth, 
                ChunkPosition.Y * ChunkRenderer.ChunkHeight, 
                ChunkPosition.Z * ChunkRenderer.ChunkWidth);
            ChunkGameObject.name = "Chunk (" + ChunkPosition.X + ", " + ChunkPosition.Y + ", " + ChunkPosition.Z + ")";
            ChunkGameObject.transform.SetParent(_planetGenerator.ChunkHolderObject.transform);
            
            _chunkRenderer = new ChunkRenderer(_planetHandler, ChunkGameObject, this);
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
                        var worldPos = ChunkToWorldPosition(new Vector3(x, y, z));
                        ChunkVoxels[x, y, z] = _planetGenerator.PickVoxel(worldPos);
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
