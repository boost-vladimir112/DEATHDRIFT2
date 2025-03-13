using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using PG;

public class CustomGameController : MonoBehaviour
{
    public TextMeshProUGUI timeScaleText;
    public Transform[] startPositions;
    public GameObject[] availableCars;
    private GameObject playerCar;

    void Start()
    {
        InitializePlayerCar();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void InitializePlayerCar()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        if (availableCars == null || availableCars.Length == 0)
        {
            Debug.LogError("No cars available to spawn!");
            return;
        }

        if (selectedCarIndex >= availableCars.Length)
        {
            Debug.LogError("Selected car index is out of range!");
            return;
        }

        playerCar = Instantiate(availableCars[selectedCarIndex]);

        if (startPositions != null && startPositions.Length > 0)
        {
            Transform spawnPoint = startPositions[Random.Range(0, startPositions.Length)];
            playerCar.transform.position = spawnPoint.position;
            playerCar.transform.rotation = spawnPoint.rotation;
        }

        AttachCameraToCar();
    }

    private void AttachCameraToCar()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No MainCamera found in the scene!");
            return;
        }

        CarController carController = playerCar.GetComponent<CarController>();
        if (carController == null)
        {
            Debug.LogError("No CarController found on the player car!");
            return;
        }

        mainCamera.transform.SetParent(playerCar.transform);
        mainCamera.transform.localPosition = new Vector3(0, 2, -5); // Настроить положение камеры
        mainCamera.transform.localRotation = Quaternion.Euler(10, 0, 0); // Настроить угол обзора
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeTimeScale(float delta)
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale + delta, 0.1f, 2f);
        if (timeScaleText)
        {
            timeScaleText.gameObject.SetActive(!Mathf.Approximately(Time.timeScale, 1));
            timeScaleText.text = $"Time scale: {Time.timeScale}";
        }
    }
}
