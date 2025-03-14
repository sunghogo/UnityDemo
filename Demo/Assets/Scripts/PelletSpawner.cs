using UnityEngine;

public class PelletSpawner : MonoBehaviour
{

    public GameObject pelletPrefab;
    public int gridWidth = 28;
    public int gridHeight = 29;
    public float spacing = 0.5f;
    public Vector2 boxSize = new Vector2(0.5f, 0.5f);
    public LayerMask _obstaclesLayer;
    public LayerMask _noSpawnsLayer;


    // Start is called before the first frame update
    void Start()
    {
         _obstaclesLayer = LayerMask.GetMask("Obstacles");
         _noSpawnsLayer = LayerMask.GetMask("No Spawns");
        GeneratePellets();
    }

    void GeneratePellets()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(transform.position.x + x * spacing, transform.position.y + y * spacing, 0);
                bool isValidSpawn = !Physics2D.OverlapBox(position, boxSize, 0f, _obstaclesLayer) && !Physics2D.OverlapBox(position, boxSize, 0f, _noSpawnsLayer);
                if (isValidSpawn) Instantiate(pelletPrefab, position, Quaternion.identity);
            }
        }
    }
}
