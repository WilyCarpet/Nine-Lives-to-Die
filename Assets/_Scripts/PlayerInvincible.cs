using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    private InvincibilityControl invincibilityControl;
    [SerializeField]
    private float duration = 2f; // Duration of invincibility in seconds
    public void Awake(){
        invincibilityControl = GetComponent<InvincibilityControl>();

    }
    public void ActivateInvincibility()
    {
        invincibilityControl.ActivateInvincibility(duration);
    }
    }
