using Unity.Entities;

namespace _Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct HyperSpaceComponent: IComponentData
    {
        public float HyperSpaceCooldown;
        public float HyperSpaceCurrentCooldown;
        public float SizeChangeSpeed;
        public bool IsHyperSpaceTravelling;
        public bool IsShrinking;
        public bool IsGrowing;
        public bool ChangePositionQueued;
    }
}