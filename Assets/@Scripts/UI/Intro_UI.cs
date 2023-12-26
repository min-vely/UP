using UnityEngine;
using UnityEngine.AddressableAssets;

public class Intro_UI : MonoBehaviour
{
    #region Init

    private void Start()
    {
        LoadAssetsWithLabels();
    }

    #endregion

    #region Methods

    private void LoadAssetsWithLabels()
    {
        string[] labels = { "Preload", "IntroScene" };

        foreach (var label in labels)
        {
            Addressables.LoadResourceLocationsAsync(label).Completed +=
                (handle) =>
                {
                    var locations = handle.Result;

                    foreach (var location in locations)
                    {
                        Addressables.InstantiateAsync(location);
                    }
                };
        }
    }

    #endregion
}
