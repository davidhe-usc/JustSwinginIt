using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grapplingRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] private int zippableLayerNumber = 8;
    [SerializeField] private LayerMask groundLayer;

    [Header("Main Camera")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public Transform groundCheck;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    [Header("Speed:")]
    [SerializeField] private float maxSpeed = 17.5f;

    private enum LaunchType
    {
        Transform_Launch,
        Zip_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float physicsSpeed = .5f;
    [SerializeField] private float transformSpeed = 10;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 1;

    [SerializeField] private float clickRadius = 10;

    [SerializeField] private float startingSpeed = 10;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    private float launchSpeed = .5f;
    private bool isGrounded = false;
    private bool wasGrounded = false;
    private float landingSpeed;

    private void Start()
    {
        grapplingRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.velocity = new Vector2(startingSpeed, 0f);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .15f, groundLayer);
        if (isGrounded)
        {
            if (!wasGrounded)
            {
                landingSpeed = m_rigidbody.velocity.x;
            }
            m_rigidbody.velocity = new Vector2(landingSpeed, 0f);
            wasGrounded = true;
        }
        else
            wasGrounded = false;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (grapplingRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grapplingRope.isGrappling)
            {
                if (launchType == LaunchType.Zip_Launch)
                {
                    Vector2 currPos = transform.position;
                    float currDistance = (grapplePoint - currPos).magnitude;
                    float currXDistance = grapplePoint.x - currPos.x;
                    float currYDistance = grapplePoint.y - currPos.y;

                    float currSpeed = m_rigidbody.velocity.magnitude;

                    if (currSpeed >= maxSpeed - (maxSpeed / 4))
                    {
                        float forceMultiplier = maxSpeed - (currSpeed / maxSpeed);
                        m_rigidbody.AddForce(grappleDistanceVector * forceMultiplier);
                    }

                    else
                    {
                        m_rigidbody.AddForce(grappleDistanceVector * launchSpeed);
                    }

                    //Debug.Log(currDistance);

                    if (currDistance <= 0.5f || (currXDistance <= 0.25f && m_rigidbody.velocity.x > launchSpeed) || (currYDistance <= 0.25f && m_rigidbody.velocity.y > launchSpeed))
                    {
                        grapplingRope.enabled = false;
                        grapplingRope.isGrappling = false;
                    }
                }

                else if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistance = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistance;
                    //Do this a different way to burst through power ups
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grapplingRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_rigidbody.gravityScale = 1;
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 click = m_camera.ScreenToWorldPoint(Input.mousePosition);
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        int grapple = 1 << grappableLayerNumber;
        int zip = 1 << zippableLayerNumber;
        filter2D.layerMask = grapple | zip;
        Physics2D.OverlapCircle(click, clickRadius, filter2D, results);

        Collider2D bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach(Collider2D result in results)
        {
            Vector2 directionToTarget = (Vector2)result.gameObject.transform.position - click;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = result;
            }
        }

        Vector2 closestPoint = bestTarget.ClosestPoint(click);
        Vector2 distanceVector = closestPoint - (Vector2)gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || _hit.transform.gameObject.layer == zippableLayerNumber || grappleToAll)
            {
                if (_hit.transform.gameObject.tag == ("Powerup"))
                {
                    launchSpeed = transformSpeed;
                    launchType = LaunchType.Transform_Launch;
                    launchToPoint = true;
                }
                else
                {
                    launchSpeed = physicsSpeed;
                    launchType = LaunchType.Physics_Launch;
                }
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    if (_hit.transform.gameObject.layer == zippableLayerNumber)
                    {
                        launchSpeed = 10;
                        launchType = LaunchType.Zip_Launch;
                        launchToPoint = true;
                    }
                    else
                    {
                        launchSpeed = 1;
                        launchType = LaunchType.Physics_Launch;
                    }

                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grapplingRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Zip_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}
