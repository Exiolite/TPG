//Copyright Ex/IO 2020

using UnityEngine;
using UnityEngine.SceneManagement;


//Скрипт для хранения игровой логики
public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public GameObject gamePlayer;
    [SerializeField] public GameObject cameraPrefab;
    [SerializeField] public GameObject gameCamera;

    [SerializeField] public GameObject floatingText;
    [SerializeField] public Color[] damageColor;
    [SerializeField] public Sprite pickUpSprite;

    [HideInInspector] public InputManager inputManager;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public VFXManager vfxManager;
    [HideInInspector] public PlayerBehaviour playerBehaviour;
    [HideInInspector] public UIManager uiManager;

    [SerializeField] public GameObject soulDrop;


    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        EventManager.playerDead.AddListener(PlayerDead);

        inputManager = GetComponent<InputManager>();
        vfxManager = GetComponent<VFXManager>();
    }

    #endregion


    void PlayerDead()
    {
        Destroy(gamePlayer.gameObject);
        Destroy(gameCamera.gameObject);
        SceneManager.LoadScene("UnTiled Labs 01", LoadSceneMode.Single);
    }

    public void InstantiatePlayer()
    {
        gameCamera = Instantiate(cameraPrefab);
        gamePlayer = Instantiate(playerPrefab);
        uiManager = FindObjectOfType<UIManager>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }
}