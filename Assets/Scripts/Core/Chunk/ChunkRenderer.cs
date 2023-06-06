using System.Collections.Generic;
using Cosmica.Core.Voxel;
using UnityEngine;

namespace Cosmica.Core.Chunk
{
    /// <summary>
    /// This class is responsible for rendering a chunk in the game world. It holds the data 
    /// related to the vertices, triangles, UVs, and other mesh information for the chunk.
    /// </summary>
    public class ChunkRenderer
    {

        public static readonly int ChunkWidth = 32;
        public static readonly int ChunkHeight = 32;

        public GameObject ChunkObject { get; }
        private PlanetChunk _planetChunk;
        private MeshRenderer _meshRenderer;
        public MeshFilter MeshFilter { get; }
        private int _vertexIndex = 0;
        public List<Vector3> ChunkVertices { get; } = new();
        public List<int> ChunkTriangles { get; } = new();
        public List<Vector2> ChunkUVs { get; } = new();
        
        private AssetRegistry _assetRegistry;

        /// <summary>
        /// Initializes a new instance of the ChunkRenderData class.
        /// </summary>
        /// <param name="chunkObject">The GameObject to which the chunk's mesh will be attached.</param>
        /// <param name="planetChunk">The PlanetChunk that holds the data for this chunk's voxels.</param>
        public ChunkRenderer(GameObject chunkObject, PlanetChunk planetChunk)
        {
            _assetRegistry = AssetRegistry.Instance;
            _planetChunk = planetChunk;
            ChunkObject = chunkObject;
            _meshRenderer = ChunkObject.AddComponent<MeshRenderer>();
            MeshFilter = ChunkObject.AddComponent<MeshFilter>();
            _meshRenderer.material = _assetRegistry.GetBlockMaterial();
            CreateMeshData();
            CreateMesh();
        }
        
        /// <summary>
        /// This method creates the mesh data for each voxel in the chunk.
        /// It goes through each voxel in the chunk and adds it to the chunk's mesh data.
        /// </summary>
        void CreateMeshData () {

            for (int y = 0; y < ChunkHeight; y++) {
                for (int x = 0; x < ChunkWidth; x++) {
                    for (int z = 0; z < ChunkWidth; z++) {
                        AddVoxelDataToChunk (new Vector3(x, y, z));
                    }
                }
            }
        }
        
        /// <summary>
        /// This method generates the actual mesh from the mesh data (vertices, triangles, UVs).
        /// It creates a new mesh, assigns the data, recalculates normals, and then sets it to the MeshFilter.
        /// </summary>
        private void CreateMesh()
        {
            Mesh mesh = new Mesh
            {
                vertices = ChunkVertices.ToArray(),
                triangles = ChunkTriangles.ToArray(),
                uv = ChunkUVs.ToArray()
            };

            mesh.RecalculateNormals();
            MeshFilter.mesh = mesh;
        }

        /// <summary>
        /// This method adds the data of a voxel (vertices, triangles, UVs) to the chunk's mesh data.
        /// </summary>
        private void AddVoxelDataToChunk(Vector3 pos)
        {
            for (int p = 0; p < 6; p++)
            {
                if (IsVoxelSolid(pos + VoxelData.FaceChecks[p]))
                {
                    continue;
                }
                int blockID = _planetChunk.ChunkVoxels[(int) pos.x, (int) pos.y, (int) pos.z];
                    
                ChunkVertices.Add(pos + VoxelData.VoxelVertices[VoxelData.TriangleVertices[p, 0]]);
                ChunkVertices.Add(pos + VoxelData.VoxelVertices[VoxelData.TriangleVertices[p, 1]]);
                ChunkVertices.Add(pos + VoxelData.VoxelVertices[VoxelData.TriangleVertices[p, 2]]);
                ChunkVertices.Add(pos + VoxelData.VoxelVertices[VoxelData.TriangleVertices[p, 3]]);
                AddTexture(_assetRegistry.BlockTypes[blockID].TextureData.GetTextureID(p));
                ChunkTriangles.Add(_vertexIndex);
                ChunkTriangles.Add(_vertexIndex + 1);
                ChunkTriangles.Add(_vertexIndex + 2);
                ChunkTriangles.Add(_vertexIndex + 2);
                ChunkTriangles.Add(_vertexIndex + 1);
                ChunkTriangles.Add(_vertexIndex + 3);
                _vertexIndex += 4;
            }
        }
        
        /// <summary>
        /// Sets the active state of the chunk.
        /// </summary>
        public bool IsActive
        {
            get => ChunkObject.activeSelf;
            set => ChunkObject.SetActive(value);
        }
        
        /// <summary>
        /// This method checks if a voxel at a specific position is outside this chunk.
        /// </summary>
        /// <param name="x">The x position of the voxel.</param>
        /// <param name="y">The y position of the voxel.</param>
        /// <param name="z">The z position of the voxel.</param>
        /// <returns>True if the voxel is outside the chunk, false otherwise.</returns>
        private bool IsVoxelOutsideChunk(int x, int y, int z)
        {
            bool outOfBounds = x < 0 || x >= ChunkWidth || y < 0 || y >= ChunkHeight || z < 0 || z >= ChunkWidth;
            return outOfBounds;
        }
        
        /// <summary>
        /// This method checks if a voxel at a specific position is solid or not.
        /// It returns true if the voxel is solid OR if the voxel is outside the chunk.
        ///
        /// IMPORTANT: This method is ONLY used to determine what faces of a voxel should be rendered.
        /// </summary>
        private bool IsVoxelSolid(Vector3 localPos) {

            int x = Mathf.FloorToInt (localPos.x);
            int y = Mathf.FloorToInt (localPos.y);
            int z = Mathf.FloorToInt (localPos.z);
            
            if (IsVoxelOutsideChunk(x, y, z))
            {
                return false;
                // return _assetRegistry.BlockTypes[_planetChunk.ChunkVoxels[x, y, z] + ];
            }

            return _assetRegistry.BlockTypes[_planetChunk.ChunkVoxels[x, y, z]].IsSolid;

        }
        
        /// <summary>
        /// This method adds the texture data to the UVs of the mesh data.
        /// It determines which part of the texture atlas to use for the voxel based on the texture ID.
        /// </summary>
        void AddTexture(int textureID) {

            float y = textureID / VoxelData.TextureAtlasSizeInBlocks;
            float x = textureID - (y * VoxelData.TextureAtlasSizeInBlocks);

            x *= VoxelData.NormalizedBlockTextureSize;
            y *= VoxelData.NormalizedBlockTextureSize;

            y = 1f - y - VoxelData.NormalizedBlockTextureSize;

            ChunkUVs.Add(new Vector2(x, y));
            ChunkUVs.Add(new Vector2(x, y + VoxelData.NormalizedBlockTextureSize));
            ChunkUVs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y));
            ChunkUVs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y + VoxelData.NormalizedBlockTextureSize));
        }
    }
}