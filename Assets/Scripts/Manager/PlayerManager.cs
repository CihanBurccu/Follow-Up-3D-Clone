using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public CameraFollow cameraFollow;

    private ParticleSystem waterVfx;
    private ParticleSystem finishVfx;
    private GameObject waterParticle;
    private GameObject finishParticle;
    private GameObject balls;
    public GameObject playerPrefab;

    private int destroyCount;
    private int instantiateCount;
    private int ballsCount = 0;
    
    List<GameObject> BallList = new List<GameObject>();

    private void Start()
    {
        waterParticle = gameObject.transform.GetChild(0).gameObject;
        waterVfx = waterParticle.GetComponent<ParticleSystem>();

        finishParticle = gameObject.transform.GetChild(1).gameObject;
        finishVfx = finishParticle.GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spawner"))
        {
            StartCoroutine(InstantiateRoutine(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(Finish(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Destroyer"))
        {
            StartCoroutine(DestroyRoutine(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("CamPos"))
        {
            transform.DOMoveZ(transform.position.z + 25, 5);
            cameraFollow.offset = new Vector3(0, 3, -4);
            cameraFollow.camDelay = 5;
        }
        else if (collision.gameObject.CompareTag("WrongColor"))
        {
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Sea"))
        {
            waterVfx.gameObject.SetActive(true);
            waterVfx.Play();
            GameOver();
        }   
    }

    IEnumerator InstantiateRoutine(GameObject playerPrefab)
    {

        instantiateCount = playerPrefab.GetComponent<Jumper>().instantiateCount;

        for (int i = 0; i < instantiateCount; i++)
        {
            ballsCount++;

            cameraFollow.offset += new Vector3(0, 0, -0.1f);

            balls = Instantiate(this.playerPrefab);

            balls.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f).OnComplete(() => 
            balls.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.1f));

            BallList.Add(balls);

            balls.AddComponent<PrefabFollow>();


            if (BallList.Count == 1)
            {
                balls.transform.position = transform.position - new Vector3(0, 0, 0.3f);
                balls.GetComponent<PrefabFollow>().connectedBall = transform.gameObject;
            }

            else
            {
                balls.transform.position = BallList[ballsCount - 2].gameObject.transform.position - new Vector3(0, 0, 0.3f);
                balls.GetComponent<PrefabFollow>().connectedBall = BallList[ballsCount - 2].gameObject;
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    IEnumerator DestroyRoutine(GameObject playerPrefab)
    {
        destroyCount = playerPrefab.GetComponent<Jumper>().destroyCount;

        for (int i = 1; i <= destroyCount; i++)
        {
            Destroy(BallList[ballsCount - 1]);
            BallList.Remove(BallList[ballsCount - 1]);

            ballsCount--;

            if (BallList.Count == 0)
            {
                GameOver();
                break;
            }

            cameraFollow.offset -= new Vector3(0, 0.1f, -0.1f);

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    IEnumerator Finish(GameObject playerPrefab)
    {
        destroyCount = playerPrefab.GetComponent<Finisher>().destroyCount;

        for (int i = 1; i <= destroyCount; i++)
        {
            Destroy(BallList[ballsCount - 1]);
            BallList.Remove(BallList[ballsCount - 1]);

            ballsCount--;

            if (BallList.Count == 0)
            {
                Success();
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    private void GameOver()
    {
        Debug.Log("Oyun bitti");

        cameraFollow.enabled = false;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        GameManager.instance.SetFailMenuState(true);
    }

    private void Success()
    {
        Debug.Log("Next Level");

        cameraFollow.enabled = false;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        finishVfx.gameObject.SetActive(true);
        finishVfx.Play();

        GameManager.instance.SetSuccessMenuState(true);
    }

}
