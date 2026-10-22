using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterial", menuName = "SuperBall/Ball Material")]
public class BallMaterialData : ScriptableObject
{
    public string materialName;
    public float mass = 1f;
    public float drag = 0.1f;
    public float angularDrag = 0.05f;
    public Material visualMaterial; // Tutaj wrzucimy materiał URP (np. czerwony, szary)
}