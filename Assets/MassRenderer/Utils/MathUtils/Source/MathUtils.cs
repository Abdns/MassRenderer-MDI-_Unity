using UnityEngine;

namespace MassRendererSystem.Utils
{
    /// <summary>
    /// Utility class for mathematical operations used in mass rendering simulations.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Generates a random position within a rectangular area on the XZ plane.
        /// Y coordinate is taken directly from the center point.
        /// </summary>
        /// <param name="center">Center point of the distribution area.</param>
        /// <param name="areaSize">Size of the area (X and Z dimensions are used).</param>
        /// <returns>A random position within the specified area bounds.</returns>
        public static Vector3 GetSpreadPosition(Vector3 center, Vector3 areaSize)
        {
            float halfX = areaSize.x * 0.5f;
            float halfZ = areaSize.z * 0.5f;

            float randomX = Random.Range(-halfX, halfX);
            float randomZ = Random.Range(-halfZ, halfZ);

            return new Vector3(center.x + randomX, center.y, center.z + randomZ);
        }
    }
}
