using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private new Renderer renderer;

    public Color Color
    {
        get => renderer.material.color;
        set => renderer.material.color = value;
    }

}
