using SFML.System;
using SFMLReady.Libraries;

namespace Generations.DefaultClasses
{
    abstract class MovingEntity : Entity
    {
        private static float factor = 0.0001f;
        public Vector2f Facing;

        public MovingEntity(Vector2f position, Vector2f facing) : base(position)
        {
            Facing = facing;
        }

        public virtual void LookAt(Entity target)
        {
            Vector2f difference = target.Position - this.Position;
            Facing = Vector2.Normalize(difference);
        }

        public virtual void Move(float speed, float seconds)
        {
            Position += Facing * speed * seconds;
        }
    }
}
