using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MaterialZone : MonoBehaviour
{
    public BallMaterialData targetMaterial;
    public float cooldown = 1f;
    private float lastTriggerTime = -999f;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastTriggerTime < cooldown) return;

        BallController ball = other.GetComponent<BallController>();
        if (ball != null && targetMaterial != null)
        {
            ball.ApplyMaterial(targetMaterial);
            lastTriggerTime = Time.time;
        }
    }
}