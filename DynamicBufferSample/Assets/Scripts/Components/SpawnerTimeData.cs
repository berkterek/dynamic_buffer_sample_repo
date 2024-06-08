using Unity.Entities;

namespace Components
{
    public struct SpawnerTimeData : IComponentData
    {
        public float CurrentTime;
        public float MaxTime;
    }
}