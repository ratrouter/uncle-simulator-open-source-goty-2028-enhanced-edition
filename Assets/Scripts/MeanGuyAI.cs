using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MeanGuyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.5f;
    public float speedIncreasePerHit = 0.05f;
    public float maxSpeed = 2f;

    [Header("Death Settings")]
    public Vector3 deathPosition = new Vector3(0, 0, 0);
    public int maxHits = 9;
    public float deathHoldTime = 3f;

    private Transform target;
    private int hits = 0;
    private bool dead = false;

    void Start()
    {
        GameObject player = GameObject.Find("FPCHARACTER");
        if (player != null)
            target = player.transform;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true; // still goes through walls
    }

    void Update()
    {
        if (dead || target == null) return;

        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        transform.LookAt(target);
    }

    public void OnHit(GameObject cube)
    {
        if (dead) return;

        hits++;
        moveSpeed = Mathf.Min(moveSpeed + speedIncreasePerHit, maxSpeed);

        if (cube != null)
            Destroy(cube);

        if (hits >= maxHits)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        float duration = 2f;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, deathPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = deathPosition;

        yield return new WaitForSeconds(deathHoldTime);

        SceneManager.LoadScene("uncle4");
    }

    // Trigger version for touching player
    void OnTriggerEnter(Collider other)
    {
        if (dead) return;

        if (other.gameObject.name == "FPCHARACTER")
        {
            SceneManager.LoadScene("gasser3");
        }
    }
}
