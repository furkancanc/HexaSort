using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
public class GridGenerator : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject hexagon;

    [Header(" Settings ")]
    [OnValueChanged("GenerateGrid")]
    [SerializeField] private int gridSize;

    private void GenerateGrid()
    {
        transform.Clear();

        for (int x = -gridSize; x <= gridSize; ++x)
        {
            for (int y = -gridSize; y <= gridSize; ++y)
            {
                Vector3 spawnPosition = grid.CellToWorld(new Vector3Int(x, y, 0));

                if (spawnPosition.magnitude > grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude * gridSize)
                {
                    continue;
                }

                GameObject gridHexInstance = (GameObject)PrefabUtility.InstantiatePrefab(hexagon);
                gridHexInstance.transform.position = spawnPosition;
                gridHexInstance.transform.rotation = Quaternion.identity;
                gridHexInstance.transform.SetParent(transform);

                //Instantiate(hexagon, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}
#endif