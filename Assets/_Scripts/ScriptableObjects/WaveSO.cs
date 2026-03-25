using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
public class WaveSO : ScriptableObject
{
    public string waveName;
    public int enemyCount;
    public float spawnCooldown;
}