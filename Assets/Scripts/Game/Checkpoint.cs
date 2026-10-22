using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public int scoreReward = 100;
    public float timeReward = 15f; // Ile sekund dodaje checkpoint
    private bool activated = false;

    void Reset() { GetComponent<Collider>().isTrigger = true; }

    void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        BallController ball = other.GetComponent<BallController>();

        // Tylko duża kula (IsLarge) aktywuje checkpoint
        if (ball != null && ball.IsLarge)
        {
            activated = true;
            GameManager.Instance.AddScore(scoreReward);
            GameManager.Instance.AddTime(timeReward);
            GameManager.Instance.SetCheckpoint(transform.position);

            Debug.Log("Checkpoint! +" + scoreReward + " pkt, +" + timeReward + " sek.");
        }
    }
}