using Strategy;
using UnityEngine;

namespace Command
{
    public class CmdRotationX : ICommand
    {
        private IMoveable _moveable;
        private Vector3 _direction;

        public CmdRotationX(IMoveable moveable, Vector3 direction)
        {
            _moveable = moveable;
            _direction = direction;
        }

        public void Execute() => _moveable.RotateX(_direction);
    
    }
}