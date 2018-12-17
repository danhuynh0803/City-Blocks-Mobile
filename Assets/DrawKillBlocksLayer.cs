using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawKillBlocksLayer : MonoBehaviour
{
    public Transform startPos, endPos;
    public int totalSpheres;
    public float sphereRadius;
    public Color gizmosColor;

    // Start is called before the first frame update
    void Start()
    {
        float distance = Vector3.Distance(startPos.position, endPos.position);
        float x = startPos.position.x;
        float y = startPos.position.y;
        float z = startPos.position.z;
        float dx = distance / (float)totalSpheres;

        for (int i = 0; i <= totalSpheres; ++i)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(x, y, z);
            sphere.transform.localScale = new Vector3(sphereRadius, sphereRadius, sphereRadius);
            x += dx;
        }
    }

}
