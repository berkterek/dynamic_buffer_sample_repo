using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct MonsterTag : IComponentData { }

    public struct MoveData : IComponentData
    {
        public float MoveSpeed;
    }

    public struct MoveDirectionData : IComponentData
    {
        public float3 Direction;
    }

    public struct RandomData : IComponentData
    {
        public Random Random;
    } 
}