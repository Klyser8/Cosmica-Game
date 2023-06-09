using UnityEngine;

namespace Cosmica.Core.Voxel
{
    public class VoxelTextureData
    {
        
        private readonly int _topFaceTexture;
        private readonly int _bottomFaceTexture;
        private readonly int _frontFaceTexture;
        private readonly int _backFaceTexture;
        private readonly int _rightFaceTexture;
        private readonly int _leftFaceTexture;
        
        public VoxelTextureData(int topFaceTexture, int bottomFaceTexture, int frontFaceTexture, int backFaceTexture, int rightFaceTexture, int leftFaceTexture)
        {
            _topFaceTexture = topFaceTexture;
            _bottomFaceTexture = bottomFaceTexture;
            _frontFaceTexture = frontFaceTexture;
            _backFaceTexture = backFaceTexture;
            _rightFaceTexture = rightFaceTexture;
            _leftFaceTexture = leftFaceTexture;
        }

        // Back, Front, Top, Bottom, Left, Right
        public int GetTextureID(int faceIndex)
        {
            switch (faceIndex)
            {
                case 0:
                    return _backFaceTexture;
                case 1:
                    return _frontFaceTexture;
                case 2:
                    return _topFaceTexture;
                case 3:
                    return _bottomFaceTexture;
                case 4:
                    return _leftFaceTexture;
                case 5:
                    return _rightFaceTexture;
                default:
                    Debug.Log("Error in GetTextureID; invalid face index");
                    return 0;
            }
        }
    }
}
