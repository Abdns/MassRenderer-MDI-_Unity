using UnityEngine;

namespace MassRendererSystem.Utils
{
    public static class MathUtils
    {
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
