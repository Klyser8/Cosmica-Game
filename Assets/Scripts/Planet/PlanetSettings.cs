using Cosmica.Core.Chunk;

namespace Cosmica.Planet
{
    /// <summary>
    /// This class holds the settings for the planet.
    /// Such settings include the width and height of the planet in chunks.
    /// This class may also hold utility methods related to the planet settings.
    /// </summary>
    public class PlanetSettings
    {
        public int PlanetWidthInChunks { get; }
        public int PlanetHeightInChunks { get; }
        
        public PlanetSettings(int planetWidthInChunks, int planetHeightInChunks)
        {
            PlanetWidthInChunks = planetWidthInChunks;
            PlanetHeightInChunks = planetHeightInChunks;
        }
        
        public int GetPlanetWidthInVoxels()
        {
            return PlanetWidthInChunks * ChunkRenderer.ChunkWidth;
        }
        
        public int GetPlanetHeightInVoxels()
        {
            return PlanetHeightInChunks * ChunkRenderer.ChunkHeight;
        }
        
    }
}