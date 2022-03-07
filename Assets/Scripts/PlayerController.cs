using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    float x = 0.0f;
    float y = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        
        transform.Translate(new Vector2(x * speed * Time.deltaTime, y * speed * Time.deltaTime));
        
    }
}
