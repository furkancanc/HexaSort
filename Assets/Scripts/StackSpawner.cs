using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform stackPositionsParent;
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private GameObject hexagonStackPrefab;

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

        int amount = Random.Range(2, 7);
        for (int i = 0; i < amount; ++i)
        {
            Vector3 hexagonLocalPos = Vector3.up * i * .2f;
            Vector3 spawnPosition = hexStack.transform.TransformPoint(hexagonLocalPos);
            GameObject hexagonInstance = Instantiate(hexagonPrefab, spawnPosition, Quaternion.identity, hexStack.transform);
        }
    }
}
