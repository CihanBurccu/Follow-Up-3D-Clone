using UnityEngine;
using DG.Tweening;

public class ChangeColor : MonoBehaviour
{
    Material playerMaterial;
    Color planeColor;

    private void Start()
    {
        planeColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerPrefab"))
        {
            playerMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
            playerMaterial.DOColor(planeColor, 0.3f);
        }
    }
}
