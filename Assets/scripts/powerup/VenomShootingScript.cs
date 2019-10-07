using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomShootingScript : Powerup
{
    public GameObject venomPrefab;

    public float maxTimeToShoot = 4.0f;
    public float shootTimer;
    public string myName;
    public int myPlayerNum;
    public List<GameObject> otherPlayers;
    private Venom venom;
    
    public void setVenom(int _myPlayerNum, GameObject _venomPrefab, List<GameObject> _otherPlayers){
        myPlayerNum = _myPlayerNum;
        venomPrefab = _venomPrefab;
        
        otherPlayers = _otherPlayers;
        
        for (int i = 0 ; i < _otherPlayers.Count;i++){
            otherPlayers[i]= _otherPlayers[i];
        }
        // Debug.Log("other players in VenomShootingScript: ");
        // for (int i = 0 ; i < otherPlayers.Count;i++){
        //     Debug.Log(otherPlayers[i].GetComponent<Movement>().playerNumber+", ");
        // }
        shootTimer = 0.0f;
        if (powerupManager == null){
            powerupManager = GetComponent<PowerupManager>();
        }
        
        if (venomPrefab != null){
            // Debug.Log("venom activated");
            if (venomPrefab.transform.position == transform.position + transform.forward ){
                // Debug.Log("position of venom prefab:" + venomPrefab.transform.position);
            }
            
            venom = venomPrefab.GetComponent<Venom>();
            venom.InitVenom(myPlayerNum, false, otherPlayers);
            
        }
        
        
    }
    

    public override void activateNow(){
        activate = true;
    }

    public override void deactivateNow(){
        Debug.Log("venom deactivated");
        activate = false;
        
        
    }

    void FixedUpdate()
    {
        if (activate && shootTimer <  maxTimeToShoot){
            venom.activateNow();
            // venom.InitVenom(myPlayerNum, true);
            Invoke("powerupInUse", maxTimeToShoot);
        }else if(shootTimer > maxTimeToShoot && activate){
            venom.deactivateNow();
            shootTimer = 0.0f;
        }
        shootTimer += Time.smoothDeltaTime;
    }

    public void powerupInUse(){
        powerupManager.powerupInUse();
        
    }
}
