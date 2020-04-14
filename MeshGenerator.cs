using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 25;
    public int zSize = 25;

    public float perlinZoom = .3f;
    public float smoothPerlin = 2f;

    // public Transform Objects;

    // public Transform[] Nature;

    public int placeAmount;

    public Transform player;

    float xLoc;
    float zLoc;

    // Start is called before the first frame update
    [ContextMenu("Run Script")]
    void RunScript(){
        Start();
    }
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        xLoc = Random.Range(5, xSize - 2);
        zLoc = Random.Range(5, zSize - 2);

        StartCoroutine("AddCollider");

        CreateShape();
        RemakeMeshToDiscrete(vertices, triangles);
        UpdateMesh();
        // PlaceObjects();
    }

    IEnumerator AddCollider(){
        yield return new WaitForSeconds(1);

        GetComponent<MeshCollider>().sharedMesh = mesh;

        yield return new WaitForSeconds(1);
    }

    public void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <=zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * perlinZoom, z * perlinZoom) * smoothPerlin;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void RemakeMeshToDiscrete(Vector3[] vert, int[] trig)
    {
        Vector3[] vertDiscrete = new Vector3[trig.Length];
        int[] trigDiscrete = new int[trig.Length];
        for (int i = 0; i < trig.Length; i++)
        {
            vertDiscrete[i] = vert[trig[i]];
            trigDiscrete[i] = i;
        }
        vertices = vertDiscrete;
        triangles = trigDiscrete;
    }

    // void PlaceObjects()
    // {
        

    //     Instantiate(Objects, new Vector3(xLoc, 1, zLoc), Quaternion.Euler(0,Random.Range(0,359), 0));

        
    //     for (int i = 0; i < placeAmount; i++)
    //     {
    //         xLoc = Random.Range(5, xSize - 2);
    //         zLoc = Random.Range(5, zSize - 2);

    //         Instantiate(Nature[Random.Range(0,Nature.Length)], new Vector3(xLoc, .5f, zLoc), Quaternion.Euler(0,Random.Range(0,359), 0));
    //     }
    // }

}
