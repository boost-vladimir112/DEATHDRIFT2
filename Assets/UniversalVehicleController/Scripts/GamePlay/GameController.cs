using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using YG;

namespace PG
{
    public class GameController : Singleton<GameController>
    {
        public TextMeshProUGUI TimeScaleText;
        public Transform[] StartPositions;
        public List<CarController> CarPrefabs; // Список префабов машин
        public bool m_SplitScreen;
        public static bool SplitScreen => Instance && Instance.m_SplitScreen && SoundHelper.SoundSupportSplitScreen && InputHelper.InputSupportSplitScreen;

        public InitializePlayer Player1 { get; private set; }
        public InitializePlayer Player2 { get; private set; }
        public CarController PlayerCar1 { get; private set; }
        public CarController PlayerCar2 { get; private set; }

        List<VehicleController> AllVehicles = new List<VehicleController>();

        void Start()
        { 

            if (SoundToggle.IsSoundEnabled())
            {
                AudioListener.pause = false;
            }
            else
            {
                AudioListener.pause = true;
            }
            
            if (TimeScaleText)
                TimeScaleText.SetActive(false);

            if (StartPositions == null || StartPositions.Length == 0)
            {
                //var respawns = GameObject.FindGameObjectsWithTag("Respawn");
                //StartPositions = respawns.Select(r => r.transform).ToArray();
            }

            int selectedCarIndex = YG2.saves.SelectedCar;
            Debug.Log($"Выбранная машина с индексом: {selectedCarIndex}");

            if (selectedCarIndex >= 0 && selectedCarIndex < CarPrefabs.Count)
            {
                PlayerCar1 = Instantiate(CarPrefabs[selectedCarIndex]); // Создаём машину из префаба
                 PlayerCar1.transform.position = StartPositions[0].position;
            }
            else
            {
                Debug.LogError("Выбранная машина не найдена, используем первую доступную.");
                PlayerCar1 = Instantiate(CarPrefabs.FirstOrDefault());
            }

          //  if (PlayerCar1 && StartPositions.Length > 0)
          //  {
           //     PlayerCar1.transform.position = StartPositions[0].position;
           //     PlayerCar1.transform.rotation = StartPositions[0].rotation;
           // }

            if (SplitScreen && CarPrefabs.Count > 1)
            {
                int secondCarIndex = (selectedCarIndex + 1) % CarPrefabs.Count;
                PlayerCar2 = Instantiate(CarPrefabs[secondCarIndex]);
                if (PlayerCar2 && StartPositions.Length > 1)
                {
                    PlayerCar2.transform.position = StartPositions[1].position;
                    PlayerCar2.transform.rotation = StartPositions[1].rotation;
                }
            }

            UpdateSelectedCars();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        void UpdateSelectedCars()
        {
            Player1 = UpdateSelectedCar(Player1, PlayerCar1);
            if (SplitScreen)
                Player2 = UpdateSelectedCar(Player2, PlayerCar2);
        }

        InitializePlayer UpdateSelectedCar(InitializePlayer player, CarController car)
        {
            if (!player)
            {
                var playerPrefab = GameSettings.IsMobilePlatform ?
                    B.ResourcesSettings.PlayerControllerPrefab_ForMobile :
                    B.ResourcesSettings.PlayerControllerPrefab;

                player = Instantiate(playerPrefab);
            }

            if (player.Initialize(car))
            {
                player.name = $"PlayerController_{player.Vehicle.name}";
                Debug.Log($"Player for {player.Vehicle.name} is initialized");
            }

            return player;
        }

        public void RestartScene()
        {
            YG2.InterstitialAdvShow();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          //  AudioListener.pause = false;
            Time.timeScale = 1f;
        }
        public void LoadMenuScene()
        {
            YG2.InterstitialAdvShow();
            SceneManager.LoadScene(0);
            
            Time.timeScale = 1f;
           // AudioListener.pause = false;
        }
        public void NextScene()
        {
            YG2.InterstitialAdvShow();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            
            Time.timeScale = 1f;
           // AudioListener.pause = false;
        }

        public void ChangeTimeScale(float delta)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + delta, 0.1f, 2f);
            if (TimeScaleText)
            {
                TimeScaleText.SetActive(!Mathf.Approximately(Time.timeScale, 1));
                TimeScaleText.text = $"Time scale: {Time.timeScale}";
            }

            SoundHelper.ChangeSoundTimeScale(Time.timeScale);
        }
    }
}