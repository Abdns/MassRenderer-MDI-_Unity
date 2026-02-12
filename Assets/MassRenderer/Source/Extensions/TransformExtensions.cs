using System.Text;
using UnityEngine;

/// <summary>
/// Extension methods for Unity Transform component.
/// </summary>
public static class TransformExtensions
{
    /// <summary>
    /// Finds a matching component in a cloned hierarchy by traversing the same path.
    /// </summary>
    public static T FindMatchingComponentIn<T>(this T originalComponent, GameObject ghostRoot, GameObject originalRoot)
        where T : Component
    {
        if (originalComponent == null || ghostRoot == null || originalRoot == null) return null;

        if (originalComponent.gameObject == originalRoot)
        {
            return ghostRoot.GetComponent<T>();
        }

        string path = GetHierarchyPath(originalComponent.transform, originalRoot.transform);
        Transform foundTransform = ghostRoot.transform.Find(path);

        return foundTransform != null ? foundTransform.GetComponent<T>() : null;
    }

    /// <summary>
    /// Gets the hierarchy path from target transform to root transform.
    /// </summary>
    public static string GetHierarchyPath(this Transform target, Transform root)
    {
        if (target == null || root == null || target == root) return string.Empty;

        var sb = new StringBuilder();
        sb.Append(target.name);

        Transform current = target.parent;
        while (current != null && current != root)
        {
            sb.Insert(0, "/");
            sb.Insert(0, current.name);
            current = current.parent;
        }

        return sb.ToString();
    }
}
