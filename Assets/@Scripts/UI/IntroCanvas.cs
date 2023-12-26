using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCanvas : MonoBehaviour
{
    #region Field

    private Button StartBtn;

    #endregion

    #region Init

    private void Start()
    {
        FindButtonAndAddListener();
    }

    #endregion

    #region Methods

    private void FindButtonAndAddListener()
    {
        StartBtn = GetComponent<Button>();

        if (StartBtn != null)
        {
            StartBtn.onClick.AddListener(LoadGameScene);
        }
        else
        {
            Debug.LogError("버튼이 없는데용");
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    #endregion
}
