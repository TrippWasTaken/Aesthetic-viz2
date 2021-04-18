using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CustomMesh : MonoBehaviour
{
    public int xSize = 1;
    public int zSize = 1;

    public float offsetX = 10f;
    public float offsetY = 10f;
    public float nBreadth = 20f;
    public float nWidth = 10f;

    int noiseScale = 2;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateShape(){
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int dSize = 4;

        float scale = 0.8f;
        for(int i = 0, z = 0; z <= zSize; z++){
            float scaleScale = xSize/2;

            for (int x = 0; x <= xSize; x++){
                if(x < xSize/2 - dSize){
                    scale = 0.8f;
                    scaleScale--;
                }
                else if(x > xSize/2 + dSize){
                        scale = 0.8f;
                        scaleScale++;
                }
                else{
                    scale = 0.1f;
                }

                float y = Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * scaleScale * scale;

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }




        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++){
            for(int x = 0; x < xSize; x++){
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
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
