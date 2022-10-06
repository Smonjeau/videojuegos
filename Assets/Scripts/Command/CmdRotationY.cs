using UnityEngine;

namespace Command
{
    public class CmdRotationY : ICommand
    {
        private IMoveable _moveable;
        private Vector3 _direction;

        public CmdRotationY(IMoveable moveable, Vector3 direction)
        {
            _moveable = moveable;
            _direction = direction;
        }

        public void Execute() => _moveable.RotateY(_direction);
    }
}