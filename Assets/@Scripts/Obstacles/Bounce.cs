using UnityEngine;

public class Bounce : MonoBehaviour
{
    #region Fields

    [SerializeField] private float force = new float();
    [SerializeField] private float stunTime = new float();

    private Vector3 hitDir;

    #endregion

    #region Methods

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (collision.gameObject.tag == "Player")
            {
                hitDir = contact.normal;
                collision.gameObject.GetComponent<PlayerController>().HitPlayer(-hitDir * force, stunTime);
                return;
            }
        }
    }

    #endregion
}