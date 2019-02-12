using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersPlayingInformation : MonoBehaviour
{
    public static PlayersPlayingInformation _playerPlayingInformation;
    public GameObject _playerPrototype;
    public Player[] _players;
    public int _maxNumberOfPlayers;

    private void Awake()
    {
        if (_playerPlayingInformation == null)
        {
            _playerPlayingInformation = this;
            _players = new Player[_maxNumberOfPlayers];

            for (int i = 0; i < _maxNumberOfPlayers; i++)
            {
                Player player = SetNewPlayerVariables(i);
                _players[i] = player;
            }

            // add a don't destroy clause
        }
        else
        {
            Destroy(this);
        }
    }


    private Player SetNewPlayerVariables(int playerNumber)
    {
        Player player = Instantiate(_playerPrototype, transform.position, Quaternion.identity).GetComponent<Player>();
        player._playerOn = false;
        player._playerNumber = playerNumber;
        player._xAxis = "Joy" + playerNumber + "X";
        player._yAxis = "Joy" + playerNumber + "Y";

        return player;
    }
}
