using SwordDefender.Game;
using UnityEngine;

//Temporary class
public class GameStarter : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.StartAction();
    }
}
