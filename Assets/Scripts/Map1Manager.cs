using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Manager : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject barrier1;

    public GameObject spawner2;
    public GameObject barrier2;

    public GameObject spawner3;

    private void OnEnable()
    {
        spawner1.SetActive(true);

        Actions.SpawnerIsEmpty += RoomDone;
    }
    private void DisEnable()
    {
        Actions.SpawnerIsEmpty -= RoomDone;
    }

    private void RoomDone(int roomNumber)
    {
        switch (roomNumber)
        {

            case 1:
                spawner1.SetActive(false);
                barrier1.SetActive(false);
                spawner2.SetActive(true);
                break;

            case 2:
                spawner2.SetActive(false);
                barrier2.SetActive(false);
                spawner3.SetActive(true);
                break;

            case 3:
                Actions.Win();
                break;

            default:
                break;

        }
    }

}
