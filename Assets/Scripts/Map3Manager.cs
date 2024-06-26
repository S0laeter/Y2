using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Manager : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject barrier1;

    public GameObject spawner2;

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
                Actions.Win();
                break;

            default:
                break;

        }

    }

}
