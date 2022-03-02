using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TerrainCollider))]
public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    TerrainData terrainData;

    [SerializeField]
    float[,] heightMap;

    [SerializeField]
    [Range (0, 0.01f)]
    float perlinStep;

    // Start is called before the first frame update
    void Start()
    {
        terrainData = GetComponent<TerrainCollider>().terrainData;

        heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        DrawTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Perlin()
    /// Assigns heightsArray values using Perlin noise
    /// </summary>
    void Perlin(float stepValue)
    {
        float xCoord = 0, yCoord = 0;

        // Fill heightArray with Perlin-based values
        for (int x = 0; x < terrainData.heightmapWidth; ++x)
        {
            for (int y = 0; y < terrainData.heightmapHeight; ++y)
            {
                heightMap[x, y] = Mathf.PerlinNoise(xCoord, yCoord);

                yCoord += stepValue;
            }

            xCoord += stepValue;
            yCoord = 0;
        }
    }

    void DrawTerrain()
    {
        // Fill the height array with values!
        Perlin(perlinStep);

        // Assign values from heightArray into the terrain object's heightmap
        terrainData.SetHeights(0, 0, heightMap);
    }
}
