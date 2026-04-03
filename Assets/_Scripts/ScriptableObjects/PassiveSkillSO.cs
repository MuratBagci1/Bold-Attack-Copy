using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillSO", menuName = "ScriptableObject/UpgradeScriptableObject", order = 6)]
public class PassiveSkillSO : UpgradeSO
{
    public float increase;
    public IncreaseType increaseType;

    public override void Apply(GameObject player)
    {
        if(!isInitiated)
        {
            //apply
        }
        else
        {
            //increase
        }
    }
}

public enum IncreaseType
{
    percentage,
    total
}