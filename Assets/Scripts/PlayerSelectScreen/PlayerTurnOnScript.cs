using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnOnScript : MonoBehaviour
{
    // get the players data script
    public List<GameObject> _playerSelectionArea;

    private void Start()
    {
        _playerSelectionArea.ForEach(playerArea => playerArea.gameObject.SetActive(false));
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2 ||
                Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2)
            {
                _playerSelectionArea[i].gameObject.SetActive(true);

                // set the player to on
                PlayersPlayingInformation._playerPlayingInformation._players[i]._playerOn = true;
            }
        }
    }
}
