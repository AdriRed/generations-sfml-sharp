using System;
using Generations.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Generations.DefaultClasses
{
    abstract class Entity : ISimulableEntity
    {
        public Vector2f Position;

        public bool canDispose;

        public virtual bool CanDispose
        {
            get
            {
                return canDispose;
            }
            set
            {
                canDispose = value;
            }
        }

        public Entity(Vector2f position)
        {
            Position = position;
        }

        public virtual void Draw(RenderWindow Window)
        {

        }
        public virtual void Update(float seconds)
        {

        }
    }
}
