using Systems.Player;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public GameObject playerPrefab;
        [SerializeField] public GameObject cameraPrefab;
        [SerializeField] public GameObject gameCamera;

        [SerializeField] public GameObject floatingText;
        [SerializeField] public Color[] damageColor;

        [HideInInspector] public InputManager inputManager;
        [HideInInspector] public PlayerBehaviour playerBehaviour;


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
            inputManager = GetComponent<InputManager>();
            InstantiatePlayer();
        }

        #endregion

        private void InstantiatePlayer()
        {
            gameCamera = Instantiate(cameraPrefab);
            playerBehaviour = Instantiate(playerPrefab).GetComponent<PlayerBehaviour>();
            playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        }
    }
}