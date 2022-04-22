using SwordDefender.CharacterControl;
using SwordDefender.Game;
using UnityEngine;

public class TestSceneManager : MonoBehaviour
{
    void Start()
    {
        GameEventManager.SendGameProcessStarted();
    }

}
