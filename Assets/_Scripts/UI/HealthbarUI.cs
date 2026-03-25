using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField] private Image healthbarImage;

    public void ChangeFillAmount(float amount)
    {
        healthbarImage.fillAmount = amount;
    }
}