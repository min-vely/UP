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
                    // 특정 라벨을 가진 모든 에셋의 리소스 위치를 비동기적으로 가져옴
                    var locations = handle.Result;

                    foreach (var location in locations)
                    {
                        // 받아온 리소스 위치를 이용해 각 리소스를 인스턴스화
                        Addressables.InstantiateAsync(location);
                    }
                };
        }
    }

    #endregion
}