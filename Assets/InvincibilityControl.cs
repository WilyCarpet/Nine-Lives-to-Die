using System.Collections;
using UnityEngine;

public class InvincibilityControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();

    }

    public void ActivateInvincibility(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        healthController.isInvincible = true;
        yield return new WaitForSeconds(duration);
        healthController.isInvincible = false;
    } 
}
