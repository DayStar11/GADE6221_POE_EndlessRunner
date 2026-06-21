using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fill;

    public void SetHealth(float current, float max)
    {
        fill.fillAmount = current / max;
    }
}
