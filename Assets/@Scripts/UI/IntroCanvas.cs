using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCanvas : MonoBehaviour
{
    #region Fields

    private Button startBtn;

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
        startBtn = GetComponent<Button>();

        if (startBtn != null)
        {
            startBtn.onClick.AddListener(delegate { LoadGameScene(); });
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
