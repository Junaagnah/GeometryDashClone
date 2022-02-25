using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem Explosion;

    [SerializeField]
    private int speed = 15;
    private float jumpAmount = 25;

    private float gravityScale = 10;

    private bool canJump = false;
    private bool shouldMove = false;
    private int nbJumps = 0; // Double jump

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            // Auto movement
            transform.position += speed * Time.deltaTime * Vector3.right;

            // Jump
            if ((canJump || nbJumps < 2) && Input.GetKeyDown(KeyCode.Space))
            {
                nbJumps += 1;
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            }
        }

        // Check player death
        if (gameObject.transform.position.y < 0)
        {
            KillPlayer();
        }
    }

    private void FixedUpdate()
    {
        // Change gravity scale
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!shouldMove) shouldMove = true; // Avoid resetting the value each time there is a collision

        rb.velocity = Vector3.zero;
        canJump = true;
        nbJumps = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }

    public void KillPlayer()
    {
        shouldMove = false;
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        
        Explosion.Play();
        Destroy(gameObject);
    }
}
