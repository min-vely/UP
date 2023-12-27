using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singileton

    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Fields

    private bool OnCheckPoint;
    [SerializeField] private Transform checkPoint;

    #endregion


}
