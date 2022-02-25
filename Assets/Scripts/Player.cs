using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int speed = 15;
    private float jumpAmount = 25;

    private float gravityScale = 10;

    private bool canJump = false;
    private bool hasTouchedGround = false; // Prevents the player from moving before init
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
        // Auto movement
        if (hasTouchedGround)
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }

        // Jump
        if ((canJump || nbJumps < 2) && Input.GetKeyDown(KeyCode.Space))
        {
            nbJumps += 1;
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Change gravity scale
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if collision on ground (is dead)
        if (collision.gameObject.CompareTag("Ground"))
        {
            KillPlayer();
        } else
        {
            if (!hasTouchedGround) hasTouchedGround = true; // Avoid resetting the value each time there is a collision

            rb.velocity = Vector3.zero;
            canJump = true;
            nbJumps = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }

    public void KillPlayer()
    {
        gameObject.SetActive(false);
    }
}
