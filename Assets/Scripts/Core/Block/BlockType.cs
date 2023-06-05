using Cosmica.Core.Voxel;

namespace Cosmica.Core.Block
{
    /// <summary>
    /// A BlockType is a type of block that can be placed in the world.
    /// It has a unique ID, a name, and a texture. The block may have other properties, such as whether it is solid or not.
    /// This class cannot be instantiated directly, instead use the Builder class.
    /// </summary>
    public class BlockType
    {
        public int BlockID { get; private set; }
        public string BlockName { get; private set; }
        public bool IsSolid { get; private set; }
        public VoxelTextureData TextureData { get; private set; }

        // Prevent public instantiation
        private BlockType() {}

        public class Builder
        {
            private int _blockID;
            private string _blockName = "default";
            private bool _isSolid = true;
            private VoxelTextureData _textureData;
            
            public Builder(int blockID)
            {
                _blockID = blockID;
            }
            
            /// <summary>
            /// Sets the name of the block.
            /// </summary>
            /// <param name="blockName">The name of the block.</param>
            /// <returns>The Builder.</returns>
            public Builder WithName(string blockName)
            {
                _blockName = blockName;
                return this;
            }

            /// <summary>
            /// Sets the block to be non-solid.
            /// </summary>
            /// <returns>The Builder.</returns>
            public Builder NonSolid()
            {
                _isSolid = false;
                return this;
            }

            /// <summary>
            /// Builds the BlockType, after the texture data has been assembled.
            /// </summary>
            /// <returns></returns>
            public BlockType Build()
            {
                AssembleTextureData();
                return new BlockType 
                { 
                    BlockID = _blockID,
                    BlockName = _blockName,
                    IsSolid = _isSolid,
                    TextureData = _textureData 
                };
            }

            /// <summary>
            /// Assembles the texture data for the block. Each block has a unique texture, with each face possibly having a unique texture.
            /// </summary>
            private void AssembleTextureData()
            {
                switch (_blockID)
                {
                    case 0:
                        _textureData = new VoxelTextureData(0, 0, 0, 0, 0, 0);
                        return;
                    case 1:
                        _textureData = new VoxelTextureData(1, 1, 1, 1, 1, 1);
                        return;
                    case 2:
                        _textureData = new VoxelTextureData(2, 2, 2, 2, 2, 2);
                        return;
                    case 3:
                        _textureData = new VoxelTextureData(4, 2, 3, 3, 3, 3);
                        return;
                    case 4:
                        _textureData = new VoxelTextureData(5, 5, 5, 5, 5, 5);
                        return;
                    default:
                        throw new System.ArgumentException("Invalid block ID! Did you forget to add a case to the switch statement?");
                }
            }
        }
    }

}