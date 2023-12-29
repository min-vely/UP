using System.Collections;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    #region Fields

    [SerializeField] private float fallTime = 3f;
    [SerializeField] private float respawnTime = 5f;

    #endregion

    #region Init

    private void Start()
    {
        InitializedObstacle();
    }

    private void InitializedObstacle()
    {
        gameObject.SetActive(true);
    }

    #endregion

    #region Methods

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Fall(fallTime));
            }
        }
    }

    private IEnumerator Fall(float fallTime)
    {
        yield return new WaitForSeconds(fallTime);
        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        //Debug.Log("발판이 생김");
    }

    #endregion
}
