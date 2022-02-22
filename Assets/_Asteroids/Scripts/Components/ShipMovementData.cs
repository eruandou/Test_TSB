using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]

    public struct ShipMovementData : IComponentData
    {
        public int MoveForward;
        public int RotateDir;
        public float RotateSpeed;
        public float Speed;
    }
}
