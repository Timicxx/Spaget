using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RayCastController : MonoBehaviour {

    public LayerMask collisionMask;
    public const float skinWidth = .015f;
    public int horizontalRays = 4;
    public int verticalRays = 4;

    [HideInInspector]
    public float horizontalRaySpacing, verticalRaySpacing;
    [HideInInspector]
    public BoxCollider2D col;
    [HideInInspector]
    public RaycastPoints raycastPoints;

    public virtual void Start()
    {
        col = GetComponent<BoxCollider2D>();
        CalcRaySpacing();
    }

    public void UpdateRaycastPoints()
    {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        raycastPoints.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastPoints.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastPoints.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastPoints.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalcRaySpacing()
    {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRays = Mathf.Clamp(horizontalRays, 2, int.MaxValue);
        verticalRays = Mathf.Clamp(verticalRays, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRays - 1);
        verticalRaySpacing = bounds.size.x / (verticalRays - 1);
    }

    public struct RaycastPoints
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
