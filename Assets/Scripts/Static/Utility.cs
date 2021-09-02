using UnityEngine;
public class Utility : MonoBehaviour
{
    public static void DrawCircle(LineRenderer line, float radius, float lineWidth)
    {
        var segments = 360;

        if (line != null)
        {
            line.useWorldSpace = false;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = segments + 1;
        }

        var pointCount = segments + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        if (line != null)
            line.SetPositions(points);
    }

    public static Vector3 RandomAsteroidCircle(Vector3 center, float radius, Vector2[] minMaxHeight, float[] beltThickness, int i)
    {
        float newRadius = UnityEngine.Random.Range(radius - beltThickness[i], radius + beltThickness[i]);
        float ang = UnityEngine.Random.value * 360;
        float newThickness = UnityEngine.Random.Range(minMaxHeight[i].x, minMaxHeight[i].y);
        Vector3 pos;
        pos.x = center.x + newRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + newThickness;
        pos.z = center.z + newRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

}
