using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

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
        string[] labels = { "IntroScene", "Preload" };

        foreach (var label in labels)
        {
            Addressables.LoadResourceLocationsAsync(label).Completed +=
                (handle) =>
                {
                    // 특정 라벨을 가진 모든 에셋의 리소스 위치를 비동기적으로 가져옴
                    var locations = handle.Result;

                    foreach (var location in locations)
                    {
                        if (location == null)
                        {
                            Debug.Log("리소스 위치가 null임");
                        }
                        else
                        {
                            Debug.Log($"로케이션: {location}");

                            // 리소스 타입이 프리팹인 경우에만 인스턴스화
                            if (location.ResourceType == typeof(GameObject))
                            {
                                Addressables.InstantiateAsync(location);
                            }
                        }
                    }
                };
        }
    }

    #endregion
}