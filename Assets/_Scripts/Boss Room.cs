using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public boss boss;
    void OnTriggerEnter2D(Collider2D collision)
    {
        boss.activateBoss();
    }
}
