using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform stackPositionsParent;
    [SerializeField] private Hexagon hexagonPrefab;
    [SerializeField] private GameObject hexagonStackPrefab;

    [Header(" Settings ")]
    [NaughtyAttributes.MinMaxSlider(2, 8)]
    [SerializeField] private Vector2Int minMaxHexCount;
    [SerializeField] private Color[] colors;

    void Start()
    {
        GenerateStacks();
    }

    private void GenerateStacks()
    {
        for (int i = 0; i < stackPositionsParent.childCount; ++i)
        {
            GenerateStack(stackPositionsParent.GetChild(i));
        }
    }

    private void GenerateStack(Transform parent)
    {
        GameObject hexStack = Instantiate(hexagonStackPrefab, parent.position, Quaternion.identity, parent);
        hexStack.name = $"Stack {parent.GetSiblingIndex()}";

        int amount = Random.Range(minMaxHexCount.x, minMaxHexCount.y);
        int firstColorHexagonCount = Random.Range(0, amount);

        Color[] colorArray = GetRandomColors();


        for (int i = 0; i < amount; ++i)
        {
            Vector3 hexagonLocalPos = Vector3.up * i * .2f;
            Vector3 spawnPosition = hexStack.transform.TransformPoint(hexagonLocalPos);
            Hexagon hexagonInstance = Instantiate(hexagonPrefab, spawnPosition, Quaternion.identity, hexStack.transform);

            hexagonInstance.Color = i < firstColorHexagonCount ? colorArray[0] : colorArray[1];
        }
    }

    private Color[] GetRandomColors()
    {
        List<Color> colorList = new List<Color>();
        colorList.AddRange(colors);

        if (colorList.Count <= 0)
        {
            Debug.LogError("No color found!");
            return null;
        }

        Color firstColor = colorList.OrderBy(x => Random.value).First();
        colorList.Remove(firstColor);

        if (colorList.Count <= 0)
        {
            Debug.LogError("Only one color was found");
        }

        Color secondColor = colorList.OrderBy(x => Random.value).First();

        return new Color[] {firstColor, secondColor};
    }
}
