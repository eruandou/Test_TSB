using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct PlayerInputData : IComponentData
    {
        public KeyCode Thrust;
        public KeyCode HyperSpace;
        public KeyCode Shoot;
        public KeyCode RotateLeft;
        public KeyCode RotateRight;
    }
}
