using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : RayCastController {

    public LayerMask PassengerMask;

    int fromWaypointIndex;
    float percentBetweenWaypoint;
    float nextMoveTime;

    public float speed;
    public bool Cyclic;
    public float waitTime;
    [Range(0, 2)]
    public float EaseVar;

    public Vector3[] localWaypoints;
    [HideInInspector]
    public Vector3[] globalWaypoints;

    List<PassengerMovement> passengerMovement;
    Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

	public override void Start () {
        base.Start();

        globalWaypoints = new Vector3[localWaypoints.Length];
        for(int i = 0; i < localWaypoints.Length; i++) {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
	}
	
	void Update () {

        UpdateRaycastPoints();

        Vector3 velocity = CalcPlatformMovement();

        CalculatePassengerMovement(velocity);

        movePassenger(true);
        transform.Translate(velocity);
        movePassenger(false);
	}

    void movePassenger(bool beforeMovePlatform) {
        foreach(PassengerMovement passenger in passengerMovement) {
            if (!passengerDictionary.ContainsKey(passenger.transform)) {
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }
            if(passenger.moveBeforePlatform == beforeMovePlatform) {
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.isStandingOnPlatform);
            }
        }
    }

    float Ease(float x) {
        float a = EaseVar + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    Vector3 CalcPlatformMovement() {

        if(Time.time < nextMoveTime) {
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoint += Time.deltaTime * speed/distanceBetweenWaypoints;
        percentBetweenWaypoint = Mathf.Clamp01(percentBetweenWaypoint);
        float easedPercent = Ease(percentBetweenWaypoint);

        Vector3 newPosition = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercent);

        if(percentBetweenWaypoint >= 1) {
            percentBetweenWaypoint = 0;
            fromWaypointIndex++;

            if (!Cyclic) {
                if(fromWaypointIndex >= globalWaypoints.Length - 1) {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }

            nextMoveTime = Time.time + waitTime;
        }

        return newPosition - transform.position;
    }

    void CalculatePassengerMovement(Vector3 velocity) {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        if (velocity.y != 0) {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (int i = 0; i < verticalRays; i++) {
                Vector2 rayOrigin = (directionY == -1) ? raycastPoints.bottomLeft : raycastPoints.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);

                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, PassengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.yellow);

                if (hit) {
                    if (!movedPassengers.Contains(hit.transform)) {
                        movedPassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                        passengerMovement.Add(
                            new PassengerMovement(
                                    hit.transform,
                                    new Vector3(pushX, pushY),
                                    directionY == 1,
                                    true
                                ));
                    }

                }
            }
        }

        if (velocity.x != 0) {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (int i = 0; i < horizontalRays; i++) {
                Vector2 rayOrigin = (directionX == -1) ? raycastPoints.bottomLeft : raycastPoints.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, PassengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.yellow);

                if (hit) {
                    if (!movedPassengers.Contains(hit.transform)) {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = -skinWidth;

                        passengerMovement.Add(
                            new PassengerMovement(
                                hit.transform,
                                new Vector3(pushX, pushY),
                                false,
                                true
                        ));
                    }
                }
            }
        }

        if(directionY == -1 || velocity.y == 0 && velocity.x != 0) {
            float rayLength = skinWidth * 2;

            for (int i = 0; i < verticalRays; i++) {
                Vector2 rayOrigin = raycastPoints.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, PassengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.yellow);

                if (hit) {
                    if (!movedPassengers.Contains(hit.transform)) {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        passengerMovement.Add(
                            new PassengerMovement(
                                hit.transform,
                                new Vector3(pushX, pushY),
                                true,
                                false
                        ));
                    }
                }
            }
        }
    }

    struct PassengerMovement {
        public Transform transform;
        public Vector3 velocity;
        public bool isStandingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _isStandingOnPlatform, bool _moveBeforePlatform) {
            transform = _transform;
            velocity = _velocity;
            isStandingOnPlatform = _isStandingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }

    private void OnDrawGizmos() {
        if(localWaypoints != null) {
            Gizmos.color = Color.green;
            float size = .3f;

            for(int i = 0; i < localWaypoints.Length; i++) {
                Vector3 globalWaypointPosition = (Application.isPlaying)? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPosition - Vector3.up * size, globalWaypointPosition + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPosition - Vector3.left * size, globalWaypointPosition + Vector3.left * size);
            }
        }
    }
}
