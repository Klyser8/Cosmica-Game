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
        public PlanetSettings PlanetSettings { get; private set; }
        public PlanetGenerator PlanetGenerator { get; private set; }

        private void Awake()
        {
            PlanetSettings = new PlanetSettings(6, 3);
            PlanetGenerator = gameObject.AddComponent<PlanetGenerator>();
        }

        /*
        public PlanetChunk GetChunkAt(Vector3 pos)
        {
            var chunkX = Mathf.FloorToInt(pos.x / ChunkRenderer.ChunkWidth);
            var chunkY = Mathf.FloorToInt(pos.y / ChunkRenderer.ChunkHeight);
            var chunkZ = Mathf.FloorToInt(pos.z / ChunkRenderer.ChunkWidth);
            return PlanetGenerator.GetChunk(chunkX, chunkY, chunkZ);
        } */
    }
}