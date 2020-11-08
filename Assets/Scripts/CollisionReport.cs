using UnityEngine;

public class CollisionReport : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameEvents.ReportGrazeChange(true);
        }
    }
}
