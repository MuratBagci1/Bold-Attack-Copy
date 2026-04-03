using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillSO", menuName = "ScriptableObject/UpgradeScriptableObject", order = 6)]
public class PassiveSkillSO : UpgradeSO
{
    public float increasePercentage;

    public override void Apply(GameObject player)
    {

    }
}