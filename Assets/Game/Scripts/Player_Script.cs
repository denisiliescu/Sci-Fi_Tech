using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarker;

    [SerializeField]
    private AudioSource _weaponAudio;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool _isReloading = false;

    private UIManager _uiManager;
    [SerializeField]
    public bool hasCoin = false;

    [SerializeField]
    private GameObject _weapon;

    public bool hasWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);

        hasWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButton(0) && currentAmmo > 0)
        {
            if(hasWeapon == true)
            {
                Shoot();
            }
        }

        else
        {

            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }

            if(Input.GetKeyDown(KeyCode.R) && _isReloading == false)
            {
                _isReloading = true;
                StartCoroutine(reloadCoolDown());
            }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        CalculateMovement();

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Shoot()
    {
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("RayCast hit: " + hitInfo.transform.name);

            GameObject hitMarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 0.2f);

        }

            _muzzleFlash.SetActive(true);
            currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);

            if (_weaponAudio.isPlaying == false)
            {
                _weaponAudio.Play();
            }


        //check if we hit the crate
        Destructible crate = hitInfo.transform.GetComponent<Destructible>();
        if(crate != null)
        {
            crate.CrateDestroy();
        }
            //destroy crate

    }
    
    public IEnumerator reloadCoolDown()
    {
;
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }
    
    public void enableWeapons()
    {
        hasWeapon = true;
        _weapon.SetActive(true);
    }


}
