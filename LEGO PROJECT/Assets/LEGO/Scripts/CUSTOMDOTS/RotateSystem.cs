using Unity.Entities; //
using Unity.Mathematics; // libraries optimized specifically for DOTS
using Unity.Transforms; // 

namespace LegoDOTS
{
    //[UpdateAfter(typeof(TranslateSystem))] -> similar to Script Execution Order
    public class RotateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var delta = quaternion.RotateY(math.radians(Time.DeltaTime * 360));

            // "in" means read only, "ref" means read/write
            // a reference to the "Rotation" component on the Entity Conversion Inspector
            Entities.ForEach((ref Rotation rotation, in Translation transform) =>
            {
                rotation.Value = math.normalize(math.mul(rotation.Value, delta));
            }).ScheduleParallel();

            // same code block as above
            Entities.WithAll<Rotate>().ForEach((ref Rotation rotation) =>
            {
                rotation.Value = math.normalize(math.mul(rotation.Value, delta));
            }).ScheduleParallel();
        }
    }
}