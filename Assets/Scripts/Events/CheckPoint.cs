using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Player _player;
    GameManager _gameManager;

    //public Vector3 playerPos;
    public List<GameObject> objectsToMove;
    public List<Vector3> objectsPos;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SetCheckPoint()
    {
        _gameManager.currCheckPoint = this;
    }

    public void LoadCheckPoint()
    {
        _player.transform.position = transform.position;
        bool isFirstDevice = true;

        for (int i = 0; i < objectsToMove.Count; i++)
        {
            objectsToMove[i].transform.position = objectsPos[i];
            if (objectsToMove[i].TryGetComponent<Device>(out Device _currDevice))
            {
                if (isFirstDevice)
                {
                    isFirstDevice = false;
                    _player.ForceSetNewDestiny(_currDevice.transform);
                }
            }
        }
    }
}
