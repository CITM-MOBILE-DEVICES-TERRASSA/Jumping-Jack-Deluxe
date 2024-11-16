using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Vector3 localStartPoint = Vector3.zero; // Æðµã
    public Vector3 localEndPoint = new Vector3(5, 0, 0); // ÖÕµã
    public float duration = 2f; 

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime*50;
        float t = Mathf.PingPong(timer / duration, 1f);
        transform.localPosition = Vector3.Lerp(localStartPoint, localEndPoint, t);
    }
}
