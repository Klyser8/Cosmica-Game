using UnityEngine;

namespace Cosmica.Core.Voxel
{
    public static class VoxelData
    {

        public static readonly int TextureAtlasSizeInBlocks = 4;

        public static readonly float NormalizedBlockTextureSize;

        static VoxelData()
        {
            NormalizedBlockTextureSize = 1f / TextureAtlasSizeInBlocks;
        }

        /// <summary>
        /// Each cube has 6 faces, with 8 vertices per cube.
        /// These Vectors are the positions of each vertex.
        /// Each cube is 1 unit in size.
        /// </summary>
        public static readonly Vector3[] VoxelVertices =
        {
            new(0.0f, 0.0f, 0.0f),
            new(1.0f, 0.0f, 0.0f),
            new(1.0f, 1.0f, 0.0f),
            new(0.0f, 1.0f, 0.0f),
            new(0.0f, 0.0f, 1.0f),
            new(1.0f, 0.0f, 1.0f),
            new(1.0f, 1.0f, 1.0f),
            new(0.0f, 1.0f, 1.0f),
        };
    
        /// <summary>
        /// Double array holding the vertices of each pair of triangles that make up each face of a cube.
        /// The second and third values are re-used for the fourth and fifth vertices.
        /// This is because as the face of the cube is composed of two triangles, each triangle shares two vertices.
        /// </summary>
        public static readonly int[,] TriangleVertices =
        {
            {0, 3, 1, 2}, // Back Face
            {5, 6, 4, 7}, // Front Face
            {3, 7, 2, 6}, // Top Face
            {1, 5, 0, 4}, // Bottom Face
            {4, 7, 0, 3}, // Left Face
            {1, 2, 5, 6} // Right Face

        };

        /// <summary>
        /// Array holding the direction of each face of a cube.
        /// The order of the directions is the same as the order of the faces in the VoxelTris array.
        /// </summary>
        public static readonly Vector3[] FaceChecks =
        {
            new(0.0f, 0.0f, -1.0f),
            new(0.0f, 0.0f, 1.0f),
            new(0.0f, 1.0f, 0.0f),
            new(0.0f, -1.0f, 0.0f),
            new(-1.0f, 0.0f, 0.0f),
            new(1.0f, 0.0f, 0.0f)
        };
    }
}
