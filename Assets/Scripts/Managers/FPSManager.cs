using UnityEngine;
using UnityEngine.InputSystem;

public class FPSManager : MonoBehaviour
{
    public static FPSManager Instance { get; private set; }

    #region Variables
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int _limitFPS = 120;
    [SerializeField] private FPSCounter _FPSCounter;
    #endregion

    #region Unity Methods
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("¡Más de un FPSManager en la escena!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = _limitFPS;
    }

    private void Update()
    {
        HandleFPSInput();
    }
    #endregion

    #region FPS
    private void HandleFPSInput()
    {
        if (_playerInput.actions["FPS"].WasPressedThisFrame())
        {
            _FPSCounter.Show();
        }
    }
    #endregion
}