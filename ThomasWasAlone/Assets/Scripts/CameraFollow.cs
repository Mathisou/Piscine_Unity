using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public PlayerController[] players;
    private Transform activePlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActivePlayer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActivePlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActivePlayer(2);
        }
    }
    private void LateUpdate()
    {
        if (activePlayer != null)
        {
            // Centrer la caméra sur le joueur actif en utilisant sa position
            Vector3 targetPosition = new Vector3(activePlayer.position.x, activePlayer.position.y, transform.position.z);
            transform.position = targetPosition;
        }
    }

    private void SetActivePlayer(int playerIndex)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].isSelected = i == playerIndex;
            if (players[i].isSelected)
            {
                activePlayer = players[i].transform;
                players[i].OnSelect?.Invoke(); // Déclenche l'événement OnSelect du joueur actif
            }
        }
    }
}

