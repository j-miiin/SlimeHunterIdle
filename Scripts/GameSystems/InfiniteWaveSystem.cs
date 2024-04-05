public class InfiniteWaveSystem : WaveSystem
{
    protected override void Update()
    {
        if (!IsWaveEnd && AliveMonsterCount < MaxSpawnCount)
        {
            AliveMonsterCount++;
            SpawnMonster();
        }
    }
}
