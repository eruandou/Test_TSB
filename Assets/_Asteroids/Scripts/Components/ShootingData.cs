using Unity.Entities;

namespace _Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct ShootingData : IComponentData
    {
        public float FireRatePerSecond;
        public float CurrentFireRate;
        public Entity BulletPrefab;
        public float BulletInitialVelocity;
    }
}
