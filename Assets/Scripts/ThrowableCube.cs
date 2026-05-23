using UnityEngine;

public class ThrowableCube : MonoBehaviour
{
    public float throwForce = 10f;
    public float holdDistance = 2f;
    public float followSpeed = 20f;

    private bool isHeld = false;
    private bool isThrown = false;

    private Transform playerCamera;
    private Rigidbody rb;

    private Collider cubeCollider;
    private Collider[] playerColliders;

    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cubeCollider = GetComponent<Collider>();
        playerCamera = Camera.main.transform;

        rb.useGravity = true;

        // Find FPCHARACTER and get ALL its colliders
        GameObject player = GameObject.Find("FPCHARACTER");
        if (player != null)
            playerColliders = player.GetComponentsInChildren<Collider>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isHeld)
                Throw();
            else
                TryPickup();
        }
    }

    void FixedUpdate()
    {
        if (isHeld)
        {
            Vector3 targetPosition = playerCamera.position + playerCamera.forward * holdDistance;
            Vector3 direction = targetPosition - rb.position;

            rb.linearVelocity = direction * followSpeed;
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isHeld = true;
                isThrown = false;
                hasHit = false;

                rb.useGravity = false;
                rb.linearDamping = 10f;
                rb.angularDamping = 10f;

                // 🔥 Ignore collision with player
                if (playerColliders != null)
                {
                    foreach (Collider pc in playerColliders)
                        Physics.IgnoreCollision(cubeCollider, pc, true);
                }
            }
        }
    }

    void Throw()
    {
        isHeld = false;
        isThrown = true;

        rb.useGravity = true;
        rb.linearDamping = 0f;
        rb.angularDamping = 0.05f;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(playerCamera.forward * throwForce, ForceMode.VelocityChange);

        // 🔥 Restore collision with player
        if (playerColliders != null)
        {
            foreach (Collider pc in playerColliders)
                Physics.IgnoreCollision(cubeCollider, pc, false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isThrown || hasHit) return;

        MeanGuyAI enemy = collision.gameObject.GetComponent<MeanGuyAI>();
        if (enemy != null)
        {
            enemy.OnHit(gameObject);
            hasHit = true;
        }
    }
}
