using System;
using UnityEngine;

namespace SwordDefender.Config
{
    [Serializable] public class CombatParams
    {
        [SerializeField] private int boxCastMaxDistance = 0;
        [SerializeField] private Vector3 checkBoxHalfSize = Vector3.zero;

        public int BoxCastMaxDistance => boxCastMaxDistance;
        public Vector3 CheckBoxHalfSize => checkBoxHalfSize;
    }
}