using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("snake")){
            Pickup(other);
        }
    }

    protected void Pickup(Collider _player){
        Debug.Log("picking up powerup");
        GameObject player = _player.gameObject;
        PowerupManager powerupManager = player.GetComponent<PowerupManager>();
        if (powerupManager != null){
            if (powerupManager.powerups.Count == 1){
            // //  uncomment to add speed
            //     Powerup speedPowerup = player.AddComponent<Speed>();
            // //  Powerup speed = this.gameObject.AddComponent<Speed>();
            //     speedPowerup.setPowerup("speed", powerupManager.myPlayerNum,true, false);
            //     powerupManager.pushPowerup(speedPowerup);
                
            //     Debug.Log("powerup type: " + speedPowerup.powerupType);
            //     Debug.Log("powerup is active: " + speedPowerup.isActive);
            //     Debug.Log("stack peek" + powerupManager.peekPowerup());
                
                //uncomment to add venom
                Powerup venomShoot = player.AddComponent<VenomShootingScript>();
                venomShoot.setPowerup("venom", powerupManager.myPlayerNum,true, false);
                powerupManager.pushPowerup(venomShoot);
                GameStateHandler.powerupsList.Remove(this.gameObject);
            
            }else if(powerupManager.powerups.Count  > 1){
                //uncomment to add venom
                powerupManager.popPowerup(); //remove current powerup
                Powerup venomShoot = player.AddComponent<VenomShootingScript>();
                venomShoot.setPowerup("venom", powerupManager.myPlayerNum,true, false);
                powerupManager.pushPowerup(venomShoot);
                GameStateHandler.powerupsList.Remove(this.gameObject);

                //uncomment to add speed
                // Powerup speedPowerup = this.gameObject.AddComponent<Speed>();
                // speedPowerup.setPowerup("speed", powerupManager.myPlayerNum , true, false);
                // powerupManager.pushPowerup(speedPowerup);
                
                // Debug.Log("powerup type: " + speedPowerup.powerupType);
                // Debug.Log("powerup is active: " + speedPowerup.isActive);
                // Debug.Log("stack peek" + powerupManager.peekPowerup());
                // GameStateHandler.powerupsList.Remove(this.gameObject);
            }
            Destroy(this.gameObject);  
        }else{
            Debug.Log("could not find powerup script component");
        }
    }
}
