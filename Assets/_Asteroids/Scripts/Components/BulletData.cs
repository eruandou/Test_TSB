using Unity.Entities;

namespace _Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct BulletData : IComponentData
    {
        public float Lifetime;
    }
}
