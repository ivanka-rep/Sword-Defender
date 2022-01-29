using System;
using UnityEngine;

namespace SwordDefender.Config
{
    [Serializable] public class CharacterStats
    {
        [SerializeField] private int damage = 0;
        [SerializeField] private int speed = 0;
        
        public int Damage => damage;
        public int Speed => speed;
    }
}