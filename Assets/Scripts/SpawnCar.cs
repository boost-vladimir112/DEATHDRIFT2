using PG;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class SpawnCar : MonoBehaviour
{
    public Transform[] StartPositions;
    public List<CarController> CarPrefabs; // ������ �������� �����
    public CarController PlayerCar1 { get; private set; }

    void Start()
    {
      
        if (StartPositions == null || StartPositions.Length == 0)
        {
            Debug.LogError("��������� ������� �� ������!");
            return;
        }

        int selectedCarIndex = YG2.saves.SelectedCar;
        Debug.Log($"��������� ������ � ��������: {selectedCarIndex}");

        if (selectedCarIndex >= 0 && selectedCarIndex < CarPrefabs.Count)
        {
            PlayerCar1 = Instantiate(CarPrefabs[selectedCarIndex]);
        }
        else
        {
            Debug.LogError("��������� ������ �� �������, ���������� ������ ���������.");
            PlayerCar1 = Instantiate(CarPrefabs.FirstOrDefault());
        }

        if (PlayerCar1 && StartPositions.Length > 0)
        {
            PlayerCar1.transform.position = StartPositions[0].position;
            PlayerCar1.transform.rotation = StartPositions[0].rotation;
        }
    }
}