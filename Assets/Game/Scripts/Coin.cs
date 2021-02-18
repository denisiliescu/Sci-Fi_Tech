using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPickUp;
    
     private void OnTriggerStay(Collider other)     //check for collision(onTriggerStay)

    {

        if (other.tag == "Player")     //check if the player has collided with the coin

        {
            if (Input.GetKeyDown(KeyCode.E))     //check for e key press

            {
                Player_Script _player = other.GetComponent<Player_Script>();
                if(_player != null)
                {
                    _player.hasCoin = true;     //give player the coin

                    AudioSource.PlayClipAtPoint(_coinPickUp, transform.position, 1f);     //play coin sound!

                    Destroy(this.gameObject);    //destroy the coin

                }
            }
        }
    }

    
}
