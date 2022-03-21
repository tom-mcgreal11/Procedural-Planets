using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSphere : MonoBehaviour
{
    public GameObject block;
    public int width = 10;
    public int height = 4;


    // Start is called before the first frame update
    void Start()
    {
        for( int i = 0; i < height; ++i)
        {
            for (int j = 0 ; j < width; j++)
            {
                Instantiate(block, new Vector3(i,j,0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
