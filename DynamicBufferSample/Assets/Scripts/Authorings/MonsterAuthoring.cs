using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Sample.Authorings
{
    public class MonsterAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        
        class MonsterBaker : Baker<MonsterAuthoring>
        {
            public override void Bake(MonsterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<MonsterTag>(entity);
                AddComponent<MoveDirectionData>(entity);

                AddComponent<MoveData>(entity, new ()
                {
                    MoveSpeed = authoring.MoveSpeed
                });

                uint seed = (uint)Mathf.Abs(entity.Index + entity.Version + new System.Random().Next());
                AddComponent<RandomData>(entity, new()
                {
                    Random = Random.CreateFromIndex(seed)
                });
            }
        }
    }    
}