using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovement : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.left; 
    public float speed = 5f;
    public float moveDuration = 3f; 

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < moveDuration)
        {
            transform.position += moveDirection.normalized * speed * Time.deltaTime;
            timer += Time.deltaTime;
        }
    }
}
