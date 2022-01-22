using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordDefender.CharacterControl.Interfaces
{
    public interface IMovementController
    {
        float Speed { get; set; }
        float RotationSpeed { get; set; }
    }
}