using Unity.Entities;

namespace Components
{
    public struct MultiMonsterSpawnerTag : IComponentData { }

    public struct MultiMonsterBuffer : IBufferElementData
    {
        public Entity Entity;
    }
}