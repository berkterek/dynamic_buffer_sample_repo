using Unity.Entities;

namespace Components
{
    public struct OneMonsterSpawnerTag : IComponentData { }

    public struct OneMonsterEntityData : IComponentData
    {
        public Entity Entity;
    }
}