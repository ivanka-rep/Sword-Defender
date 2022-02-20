using SwordDefender.Game;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.StartAction();
    }
}
