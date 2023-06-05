using System;
using UnityEngine;

namespace Cosmica.Core.Chunk
{
    public class PlanetChunk
    {
        
        private GameObject _chunkGameObject;
        private ChunkRenderData _chunkRenderData;
        public int[,,] ChunkVoxels { get; } = new int[ChunkRenderData.ChunkWidth, ChunkRenderData.ChunkHeight, ChunkRenderData.ChunkWidth];

        private AssetRegistry _assetRegistry;
        
        public PlanetChunk()
        {
            _assetRegistry = AssetRegistry.Instance;
            PopulateChunkVoxels();
            _chunkGameObject = new GameObject();
            _chunkRenderData = new ChunkRenderData(_chunkGameObject, this);
        }

        /// <summary>
        /// This method populates the ChunkVoxels array, with the ID of each voxel.
        /// It iterates through each voxel in the chunk and assigns it a default value.
        /// </summary>
        private void PopulateChunkVoxels()
        {
            for (int y = 0; y < ChunkRenderData.ChunkHeight; y++)
            {
                for (int x = 0; x < ChunkRenderData.ChunkWidth; x++)
                {
                    for (int z = 0; z < ChunkRenderData.ChunkWidth; z++)
                    {
                        ChunkVoxels[x, y, z] = 3;
                    }
                }
            }
        }

    }
}
