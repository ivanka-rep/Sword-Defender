using System;
using UnityEngine;

namespace SwordDefender.Config
{
    [Serializable] public class EnemyParams
    {
        [SerializeField] private int damage = 0;
        [SerializeField] private int speed = 0;
        [SerializeField] private float distanceToPlayerOffset = 0f;
        [SerializeField] private int enemiesAmount = 0;
        
        public int Damage => damage;
        public int Speed => speed;
        public float DistanceToPlayerOffset => distanceToPlayerOffset;

        public int EnemiesAmount => enemiesAmount;
    }
}