using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MonsterMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            var job = new MonsterMoveJob()
            {
                DeltaTime = deltaTime
            };

            var jobHandle = job.ScheduleParallel(state.Dependency);
            state.Dependency = jobHandle;
        }
    }

    [BurstCompile]
    public partial struct MonsterMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        private void Execute(Entity entity, in MoveData moveData, in MoveDirectionData moveDirectionData, ref LocalTransform localTransform)
        {
            localTransform.Position += moveData.MoveSpeed * DeltaTime * moveDirectionData.Direction;
        }
    }
}