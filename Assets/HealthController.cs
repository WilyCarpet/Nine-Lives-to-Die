using UnityEngine;
using UnityEngine.Events;
public class HealthController : MonoBehaviour
{
    [SerializeField]
private float curr_health;
[SerializeField]
private float max_health;

public bool isInvincible{get; set;} 

public float RemainingHealth {
    get{
        return curr_health / max_health;    
        }
}

public UnityEvent onDeath;

public UnityEvent onDamage;

public UnityEvent OnHealthChange;

public void TakeDamage(float damage){
   
    if(curr_health == 0){
        return;
    }
    if(isInvincible){
        return;
    }
     curr_health -= damage;

     OnHealthChange.Invoke();

     if(curr_health < 0){
        curr_health = 0;

        // Handle death logic here, e.g., destroy the object or trigger a death animation
     }
     if(curr_health == 0){
        onDeath.Invoke();
     }else{
        onDamage.Invoke();
     }

}

public void Heal(float addAmount){
    if(curr_health == max_health){
        return;
    }

    curr_health += addAmount;

    OnHealthChange.Invoke();
    
    if(curr_health > max_health){
        curr_health = max_health;
    }

}
}


