using System.Collections.Generic;
using Controllers;
using Flyweight;

namespace Strategy
{
    public interface ILevel
    {
        public string LevelName { get; }
        public bool IsFinalLevel { get; }
        public void StartLevel();
        public void EndLevel();
    }
}