using UnityEngine;

public class Main : MonoBehaviour
{
    #region Singileton

    private static Main _instance;
    private static bool _initialized;
    private static Main Instance
    {
        get
        {
            if (!_initialized)
            {
                _initialized = true;

                GameObject main = GameObject.Find("@Main");
                if (main == null)
                {
                    main = new GameObject { name = "@Main" };
                    main.AddComponent<Main>();
                    DontDestroyOnLoad(main);
                    _instance = main.GetComponent<Main>();
                }
            }

            return _instance;
        }
        
    }
    #endregion

    #region Fields

    private readonly PlayerController _playerControl = new();
    //private readonly GameManager _game = new();
    //private SoundManager _sound = new();

    #endregion

    #region Properties
    public static PlayerController PlayerControl => Instance._playerControl;
    //public static GameManager Game => Instance._game;
    //public static SoundManager Sound
    //{
    //    get => Instance._sound;
    //    set => Instance._sound = value;
    //}

    #endregion
}