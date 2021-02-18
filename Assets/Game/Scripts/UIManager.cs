using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    public Image _coinImage;
    private Player_Script _player;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player_Script>();
    }

    private void Update()
    {

        if (_player.hasCoin == true)
        {
            _coinImage.gameObject.SetActive(true);
        }
        
        if(_player.hasWeapon == true)
        {
            _ammoText.gameObject.SetActive(true);
        }
        else
        {

            _ammoText.gameObject.SetActive(false);
        }


    }


    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }




}
