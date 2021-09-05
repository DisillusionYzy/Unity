using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private float rotateSpeed;
    
    void Start()
    {
        var x = Random.Range(-5.0f, 5.0f);
        var y = Random.Range(-5.0f, 5.0f);
        var z = Random.Range(-1.0f, 1.0f);
        transform.position = new Vector3(x, y, z);

        var scale = Random.Range(0.5f, 2.0f);
        transform.localScale = Vector3.one * scale;
        
        Material material = Renderer.material;
        var r = Random.Range(0.0f, 1.0f);
        var g = Random.Range(0.0f, 1.0f);
        var b = Random.Range(0.0f, 1.0f);
        var a = Random.Range(0.0f, 1.0f);
        material.color = new Color(r, g, b, a);

        rotateSpeed = Random.Range(5.0f, 20.0f);
    }
    
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
