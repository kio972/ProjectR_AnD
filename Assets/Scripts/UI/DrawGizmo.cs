using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawGizmo : MonoBehaviour
{
    public enum GizmoType { Box, Sphere };
    public GizmoType gizmoType = GizmoType.Box;
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 1.0f;
    public float yOffset = 0.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        if (gizmoType == GizmoType.Box)
        {
            Gizmos.DrawWireCube(transform.position + Vector3.up * yOffset, Vector3.one * gizmoSize);
        }
        else if (gizmoType == GizmoType.Sphere)
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.up * yOffset, gizmoSize);
        }
    }
}
