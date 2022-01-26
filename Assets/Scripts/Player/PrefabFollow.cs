using UnityEngine;

public class PrefabFollow : MonoBehaviour
{
    public GameObject connectedBall;

    private ParticleSystem waterVfx;
    private GameObject particlePrefab;

    private void Start()
    {
        particlePrefab = gameObject.transform.GetChild(0).gameObject;
        waterVfx = particlePrefab.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, connectedBall.transform.position.x, Time.deltaTime * 15),
            Mathf.Lerp(transform.position.y, connectedBall.transform.position.y, Time.deltaTime * 18),
            connectedBall.transform.position.z - 0.4f
            );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
            waterVfx.gameObject.SetActive(true);
            waterVfx.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
