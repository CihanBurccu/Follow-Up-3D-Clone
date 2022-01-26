using UnityEngine;

public class Jumper : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Vector3 jumpForce;

    public bool destroyBall = false;
    public int destroyCount = 0;

    public int instantiateCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.Jump(jumpForce);
        }
    }
}
