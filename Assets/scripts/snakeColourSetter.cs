using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class snakeColourSetter
{
   
    public static void SetColor(MeshRenderer meshRenderer, Texture texture)
    {
        // Debug.Log("Colours colour number " + colourNumber);
        meshRenderer.materials[0].mainTexture = texture;
        
    }

}
