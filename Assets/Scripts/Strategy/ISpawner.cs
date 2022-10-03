
public interface ISpawner
{
    float SpawnDelay { get; set; }
    int SpawnCount { get; set; }
    
    void Spawn();
}