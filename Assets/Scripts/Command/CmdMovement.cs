using Strategy;
using UnityEngine;

namespace Command
{
    public class CmdMovement : ICommand
    {
        // propiedades
        private IMoveable _moveable;
        private Directions _direction;

        public CmdMovement(IMoveable moveable, Directions direction)
        {
            _moveable = moveable;
            _direction = direction;
        }

        public void Execute() => _moveable.Travel(_direction);
    }
}