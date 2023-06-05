using System;
using Cosmica.Core.Block;
using UnityEngine;

namespace Cosmica.Core
{
    /// <summary>
    /// This class is a singleton that holds references to all assets used in the game.
    /// </summary>
    [Serializable]
    public class AssetRegistry : MonoBehaviour
    {
        [SerializeField] private Material blockMaterial;

        public BlockType[] BlockTypes { get; } =
        {
            new BlockType.Builder(0).WithName("Air").NonSolid().Build(),
            new BlockType.Builder(1).WithName("Bedrock").Build(),
            new BlockType.Builder(2).WithName("Dirt").Build(),
            new BlockType.Builder(3).WithName("Grass").Build(),
            new BlockType.Builder(4).WithName("Stone").Build(),
        };

        public static AssetRegistry Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Duplicate AssetRegistry found! Destroying this one...");
                Destroy(this);
            }
        }

        public Material GetBlockMaterial()
        {
            return blockMaterial;
        }
    }
}