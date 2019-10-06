using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class snakeColourSetter
{

    public static void SetColor(int colourNumber, SkinnedMeshRenderer meshRenderer)
    {
            // Debug.Log("Colours colour number " + colourNumber);
        switch (colourNumber)
        {
            case 0:
               meshRenderer.materials[0].color = Color.red;
                
                break;
            case 1:
                meshRenderer.materials[0].color = Color.blue ;
                break;
            case 2:
                meshRenderer.materials[0].color = Color.green;
                break;
            case 3:
                meshRenderer.materials[0].color = Color.yellow ;
                break;
            case 4:
                //AI
                meshRenderer.materials[0].color = Color.black;
                break;
            default:
                meshRenderer.materials[0].color = Color.white;
                break;
        }
        
    }

}
