using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{   [SerializeField] private Vector2 parallaxEffectMultiplier;

    
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    // Start is called before the first frame update
    void Start()
    {
        
        cameraTransform = Camera.main.transform;  // reference to main camera
        lastCameraPosition = cameraTransform.position; // initial camera position
        Sprite sprite = GetComponent<SpriteRenderer>().sprite; // Get sprite from this
        Texture2D texture = sprite.texture;                 // get texture
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit; // size of each unit of the texture in x
        textureUnitSizeY = texture.height  / sprite.pixelsPerUnit;
        // Debug.Log(texture.width + "\n" + sprite.pixelsPerUnit + "\n"+ textureUnitSizeX);
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition  = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x ) >= textureUnitSizeX )
        {
            float offSetPositionX = ( cameraTransform.position.x - transform.position.x ) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offSetPositionX, transform.position.y);
        }

        if (Mathf.Abs(cameraTransform.position.y - transform.position.y ) >= textureUnitSizeY )
        {
            float offSetPositionY = ( cameraTransform.position.y - transform.position.y ) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offSetPositionY);
        }  
    }
}
