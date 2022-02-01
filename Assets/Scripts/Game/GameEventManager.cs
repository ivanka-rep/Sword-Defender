using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SwordDefender.Game
{
    public class GameEventManager
    {
        public static readonly UnityEvent<int> OnPlayerHealthChanged = new UnityEvent<int>();

        public static void SendPlayerHealthChanged(int health)
        {
            OnPlayerHealthChanged.Invoke(health);
        }
    }
}