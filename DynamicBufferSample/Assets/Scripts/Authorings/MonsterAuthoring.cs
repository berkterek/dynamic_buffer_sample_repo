using Components;
using Unity.Entities;
using UnityEngine;

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
            }
        }
    }    
}