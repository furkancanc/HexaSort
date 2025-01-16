using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new Collider collider;
    public HexStack HexStack { get; private set; }
    public Color Color
    {
        get => renderer.material.color;
        set => renderer.material.color = value;
    }

    public void Configure(HexStack hexStack)
    {
        HexStack = hexStack;
    }

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
