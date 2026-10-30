using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // Kula
    public float distance = 7f;
    public float height = 3f;
    public float rotateSpeed = 100f;

    private float currentYaw = 90f;
    private float currentPitch = 20f; // Kąt patrzenia z góry (20 stopni)

    void LateUpdate()
    {
        if (target == null) return;

        // Reakcja tylko na strzałki
        float inputX = 0f;
        float inputY = 0f;

        if (Input.GetKey(KeyCode.RightArrow)) inputX = 1f;
        if (Input.GetKey(KeyCode.LeftArrow)) inputX = -1f;
        if (Input.GetKey(KeyCode.UpArrow)) inputY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) inputY = -1f;

        currentYaw += inputX * rotateSpeed * Time.deltaTime;
        currentPitch -= inputY * rotateSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, 5f, 80f);

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = target.position + offset + (Vector3.up * height);
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }
}