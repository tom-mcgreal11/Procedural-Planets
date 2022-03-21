using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldOut;
    [HideInInspector]
    public bool colourSettingsFoldOut;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator =  new ColourGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Terrain[] terrainFaces;
    
    void Initialize(){

        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);

        if(meshFilters == null || meshFilters.Length == 0){
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new Terrain[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward , Vector3.back};
        for( int i = 0; i < 6; i++){
            
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

            terrainFaces[i] = new Terrain(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }

    }

    public void GeneratePlanet(){
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated(){
        if(autoUpdate){
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh(){
        foreach (Terrain face in terrainFaces){
            face.ConstructMesh();
        }
        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours(){
        colourGenerator.UpdateColours();
        foreach (Terrain face in terrainFaces){
            face.UpdateUVs(colourGenerator);
        }
    }

}
