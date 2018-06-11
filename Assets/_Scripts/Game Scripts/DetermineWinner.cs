using Characters;
using Managers;
using Player.Management;
using UnityEngine;
using UnityEngine.UI;

public class DetermineWinner : MonoBehaviour
{
    [SerializeField] private Transform[] m_positions = null;
    [SerializeField] private Text m_victoryText = null;

    // Use this for initialization
    private void Awake()
    {
        GameObject[] players = new GameObject[PlayerSettings.Length];

        for (int i = 0; i < players.Length; i++)
            players[i] = PlayerSettings.GetCharacter(i);

        FindWinner(players);
    }

    private void FindWinner(GameObject[] players)
    {
        int firstPlace = 1;
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i + 1; j < players.Length; j++)
            {
                if(players[i].GetComponent<Death>().NumberOfLives > players[j].GetComponent<Death>().NumberOfLives)
                {
                    GameObject temp = players[i];
                    players[i] = players[j];
                    players[j] = temp;
                    firstPlace = j + 1;
                }
            }
        }

        for (int i = 0; i < m_positions.Length; i++)
        {
            if (i >= players.Length)
                break;
            Instantiate(players[i], m_positions[i], false);
        }

        m_victoryText.text = "Player " + PlayerManager.Placement[0] + " wins!";
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
