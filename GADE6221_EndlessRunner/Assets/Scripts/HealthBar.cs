using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;

    public void SetHealth(float current, float max)
    {
        fill.fillAmount = current / max;
    }
}
