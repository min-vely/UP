using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class Game_Instance : Base_Instance
{
    #region Methods

    protected override string[] GetLabels()
    {
        return new string[] { "GameScene", "Preload" };
    }

    #endregion
}