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

    public int noiseScale = 2;

    Mesh mesh;
    Vector3[] vertices;
    int[] trinagles;
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

        for(int i = 0, z = 0; z <= zSize; z++){
            for (int x = 0; x <= xSize; x++){

                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * noiseScale;

                vertices[i] = new Vector3(x, y, z);
                // if( x > 16 && x < 24) {
                //     // vertices[i] = new Vector3(x, 0, z);
                //     noiseScale = 0;
                // }

                if(x < 16){
                    if(noiseScale > 0 )
                        noiseScale--;
                }
                else if(x > 24){
                        noiseScale++;
                }
                else{
                    noiseScale = 0;
                }

                i++;
            }
        }
        trinagles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++){
            for(int x = 0; x < xSize; x++){
                trinagles[tris + 0] = vert + 0;
                trinagles[tris + 1] = vert + xSize + 1;
                trinagles[tris + 2] = vert + 1;
                trinagles[tris + 3] = vert + 1;
                trinagles[tris + 4] = vert + xSize + 1;
                trinagles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
        vert++;
        }

    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = trinagles;

        mesh.RecalculateNormals();
    }
}
