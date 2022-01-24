using System;
using System.Collections.Generic;
using SwordDefender.CharacterControl;
using UnityEngine;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform playerT;
        [SerializeField] private List<EnemyController> enemyList;
        private void Awake()
        {
        //Todo: отдельный контроллер противников
        enemyList.ForEach(enemy => { enemy.StartMoving(playerT); });
        }
    }
}