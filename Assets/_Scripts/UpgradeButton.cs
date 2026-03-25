using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : ButtonBase
{
    protected override void ButtonPressed()
    {
        GetComponentInParent<UpgradeSingleUI>().OnUpgradeSelected();
    }
}