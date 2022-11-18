
using Flyweight;

namespace Strategy
{
    public interface ISpawner
    {
        void Spawn(ZombieStats stats, float zombieLifeIncrement);
    }
}