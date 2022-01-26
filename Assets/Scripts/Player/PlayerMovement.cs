using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    SwipeManager swipeManager;
    Rigidbody rigidbody;
    Animator animator;

    public float moveSpeed = 15f;
    private bool isFirtTap;

    void Start()
    {
        animator = GetComponent<Animator>();
        swipeManager = GetComponent<SwipeManager>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        isFirtTap = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isFirtTap)
        {
            animator.enabled = false;
            rigidbody.useGravity = true;
            isFirtTap = false;
        }

        Move();
    }

    void Move()
    {
        transform.position = transform.position + swipeManager.difference * moveSpeed * Time.deltaTime;
    }

    public void Jump(Vector3 jump)
    {
        rigidbody.AddForce(jump, ForceMode.Impulse);
    }
}
