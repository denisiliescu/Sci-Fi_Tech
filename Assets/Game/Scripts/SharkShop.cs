using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    //check for collision
    //check if the player
    //check for e key
    //check if player has coin
    //remove coin from player and inventory
    //play win sound
    //debug you have no coin

    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _winAudio;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player_Script player = other.GetComponent<Player_Script>();
            if(player != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (player.hasCoin == true)
                    {
                        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                        if (_uiManager != null)
                        {
                            _uiManager._coinImage.gameObject.SetActive(false);
                            player.hasCoin = false;
                        }
                        AudioSource.PlayClipAtPoint(_winAudio, Camera.main.transform.position, 1f);
                        //play sound
                        player.enableWeapons();


                    }
                    else
                    {
                        Debug.Log("You have no coins!");
                    }
                }
            }
           
        }








    }





}
