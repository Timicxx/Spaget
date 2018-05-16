using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller2D : RayCastController {

    public float maxClimbAngle = 60;
    public float maxDescendAngle = 50;

    public CollisionInfo collisions;

    public List<GameObject> traps = new List<GameObject>();
    Player player;

    public override void Start() {
        base.Start();
        player = transform.GetComponent<Player>();
    }

    public void Move(Vector3 velocity, bool isStandingOnPlatform = false) {
        CalcRaySpacing();
        UpdateRaycastPoints();
        collisions.Reset();

        collisions.velocityPrevious = velocity;

        if(velocity.y < 0) {
            DescendSlope(ref velocity);
        }
        if(velocity.x != 0 | velocity.x == 0) {
            HorizontalCollision(ref velocity);
        }
        if(velocity.y != 0 | velocity.y == 0) {
            VerticalCollision(ref velocity);
        }
        if (isStandingOnPlatform) {
            collisions.below = true;
        }
        
        transform.Translate(velocity);
    }

    void VerticalCollision(ref Vector3 velocity) {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRays; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycastPoints.bottomLeft : raycastPoints.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) {

                player.HitHandler(hit, traps);

                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope) {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope) {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x + skinWidth);
            Vector2 rayOrigin = ((directionX == -1) ? raycastPoints.bottomLeft : raycastPoints.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit) {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle != collisions.slopeAngle) {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    void HorizontalCollision(ref Vector3 velocity) {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRays; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastPoints.topLeft : raycastPoints.topRight;
            rayOrigin += Vector2.down * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit) {

                player.HitHandler(hit, traps);

                if (hit.distance == 0) {
                    continue;
                }

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (i > 0 && slopeAngle <= maxClimbAngle) {
                    if (collisions.descendingSlope) {
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityPrevious;
                    }
                    float distanceToSlopeStart = 0;
                    if(slopeAngle != collisions.slopeAnglePrevious) {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.x += distanceToSlopeStart * directionX;
                }

                if(!collisions.climbingSlope || slopeAngle > maxClimbAngle) { 
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope) {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle) {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        if(velocity.y <= climbVelocityY) {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
        
    }

    void DescendSlope(ref Vector3 velocity) {
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastPoints.bottomRight : raycastPoints.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit) {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle != 0 && slopeAngle <= maxDescendAngle) {
                if(Mathf.Sign(hit.normal.x) == directionX) {
                    if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)) {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    public struct CollisionInfo {
        public bool above, below;
        public bool left, right;
        public bool climbingSlope, descendingSlope;
        public float slopeAngle, slopeAnglePrevious;
        public Vector3 velocityPrevious;

        public void Reset() {
            above = below = false;
            left = right = false;
            climbingSlope = descendingSlope = false;
            slopeAnglePrevious = slopeAngle;
            slopeAngle = 0;
        }
    }
}
