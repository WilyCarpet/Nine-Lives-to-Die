using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image healthBarImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateHealthBar(HealthController healthController)
    {
        // Calculate the health percentage
        healthBarImage.fillAmount = healthController.RemainingHealth;
    }
}
