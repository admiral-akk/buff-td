using UnityEngine;

public class MarkingTurret : MonoBehaviour
{
    private float range = 3f;

    private void Update()
    {
        ApplyDebuff();
    }

    protected void ApplyDebuff()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                MarkerDebuff markerDebuff = collider.gameObject.GetComponent<MarkerDebuff>();
                if (markerDebuff != null)
                {
                    markerDebuff.timeLeft = 0.3f;
                } else
                {
                    collider.gameObject.AddComponent(typeof(MarkerDebuff));
                }
            }
        }
    }
}
