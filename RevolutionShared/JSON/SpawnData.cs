using RevolutionShared.Rose.Data;
using System.Collections.Generic;
using System;

[Serializable]
public class SpawnData : IData
{
    public int ID;
    public string MapName;
    public List<EnemySpawner> Spawners;

    int IData.ID { get => ID; set => ID = value; }
}

[Serializable]
public class EnemySpawner
{
    public SpawnSettings Settings;
    public List<EnemySpawn> Basic;
    public List<EnemySpawn> Tactic;
}

[Serializable]
public class EnemySpawn
{
    public int ID;
    public int Count;
    public string Description;
}

[Serializable]
public class SpawnSettings
{
    public string Name;
    public float MapX;
    public float MapY;
    public int ID;
    public float WorldX;
    public float WorldY;
    public float WorldZ;
    public int Interval;
    public int LimitCount;
    public float Range;
    public int TacticPoints;
}