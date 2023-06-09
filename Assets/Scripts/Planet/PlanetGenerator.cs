using System;
using System.Collections.Generic;
using Cosmica.Core.Chunk;
using UnityEngine;

namespace Cosmica.Planet
{
    /// <summary>
    /// This class is responsible for generating the planet.
    /// 
    /// It also holds a list of all the chunks that have been generated.
    /// </summary>
    public class PlanetGenerator : MonoBehaviour
    {
        private PlanetHandler _planetHandler;
        public GameObject ChunkHolderObject { get; private set; }
        public PlanetChunk[,,] Chunks { get; private set; }

        private void Awake()
        {
            _planetHandler = gameObject.GetComponent<PlanetHandler>();
            Chunks = new PlanetChunk[
                _planetHandler.PlanetSettings.PlanetWidthInChunks, 
                _planetHandler.PlanetSettings.PlanetHeightInChunks, 
                _planetHandler.PlanetSettings.PlanetWidthInChunks];
            ChunkHolderObject = new GameObject
            {
                name = "Chunks",
            };
            ChunkHolderObject.transform.SetParent(transform);
        }

        private void Start()
        {
            GenerateInitialChunks();
        }

        private void GenerateInitialChunks()
        {
            for (int y = 0; y < _planetHandler.PlanetSettings.PlanetHeightInChunks; y++)
            {
                for (int x = 0; x < _planetHandler.PlanetSettings.PlanetWidthInChunks; x++)
                {
                    for (int z = 0; z < _planetHandler.PlanetSettings.PlanetWidthInChunks; z++)
                    {
                        GenerateChunk(x, y, z);
                    }
                }
            }
        }
        
        private void GenerateChunk(int x, int y, int z)
        {
            Chunks[x, y, z] = new PlanetChunk(_planetHandler, x, y, z);
        }

        public int PickVoxel(Vector3 pos)
        {
            if (!IsVoxelInPlanet(pos))
            {
                return 0;
            }

            int x = (int) pos.x;
            int y = (int) pos.y;
            int z = (int) pos.z;
            if (y == 4 && x == _planetHandler.PlanetSettings.GetPlanetWidthInVoxels() - 1 && z == _planetHandler.PlanetSettings.GetPlanetWidthInVoxels() - 1)
            {
                return 1;
            }
            if (y == 0)
            {
                return 1;
            }

            if (y < _planetHandler.PlanetSettings.GetPlanetHeightInVoxels() - 4)
            {
                return 4;
            }
            if (y < _planetHandler.PlanetSettings.GetPlanetHeightInVoxels() - 1)
            {
                return 2;
            }
            return 3;
        }
        
        /// <summary>
        /// Checks whether the chunk at the given chunk position is in the planet.
        /// </summary>
        /// <param name="chunkPos">The chunk position to check.</param>
        /// <returns>True if the chunk is within the planet, false otherwise.</returns>
        private bool IsChunkInPlanet(ChunkPosition chunkPos)
        {
            var chunkInPlanet = chunkPos.X >= 0 && chunkPos.X < _planetHandler.PlanetSettings.PlanetWidthInChunks &&
                               chunkPos.Y >= 0 && chunkPos.Y < _planetHandler.PlanetSettings.PlanetHeightInChunks &&
                               chunkPos.Z >= 0 && chunkPos.Z < _planetHandler.PlanetSettings.PlanetWidthInChunks;
            return chunkInPlanet;
        }
        
        private bool IsVoxelInPlanet(Vector3 voxelPos)
        {
            var voxelInPlanet = voxelPos.x >= 0 && voxelPos.x < _planetHandler.PlanetSettings.GetPlanetWidthInVoxels() &&
                                voxelPos.y >= 0 && voxelPos.y < _planetHandler.PlanetSettings.GetPlanetHeightInVoxels() &&
                                voxelPos.z >= 0 && voxelPos.z < _planetHandler.PlanetSettings.GetPlanetWidthInVoxels();
            return voxelInPlanet;
        }

    }
}