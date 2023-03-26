using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float jumpPower = 0.3f;
    public float fallMultiplier = 1f;
    public int numJump = 1;


    public Animator animator;
    public float rotateSpeed = 5f;

    private Rigidbody rb;
    public bool isGrounded;
    private bool isCrouching;

    RagdollManager ragdollScript;
    CapsuleCollider mainCollider;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        
        animator = GetComponent<Animator>();
        ragdollScript = GetComponent<RagdollManager>();
        mainCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (!ragdollScript.ragdollisOn)
        {
                // Movement
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");
                Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed;
                if (!isCrouching)
                rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

                // Better Fall from Jump
                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector3.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
                }

                // Rotation
                if (movement != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(movement);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                }

                // Animation
                if (movement.magnitude > 0f)
                {
                    animator.SetInteger("index", 1);

                }
                else
                {

                    animator.SetInteger("index", 0);
                }

                // Jumping
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
                {
                    isGrounded = false;
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    animator.SetTrigger("Jump");


                }


            // Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true;

                animator.SetInteger("index", 2);
                animator.SetBool("isCrouching", isCrouching);
                mainCollider.height = 2.2f;

            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isCrouching = false;
                animator.SetInteger("index", 0);
                animator.SetBool("isCrouching", isCrouching);
                mainCollider.height = 2.7f;
            }

        }


    }

    void FixedUpdate()
    {


        // Check if player is on the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.2f))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(ragdollScript.RagdollOn());
            animator.SetInteger("index", 0);
            isCrouching = false;
            animator.SetBool("isCrouching", isCrouching);
            mainCollider.height = 2.7f;
        }
    }
}
