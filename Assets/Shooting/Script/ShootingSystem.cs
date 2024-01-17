using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
//[ExecuteInEditMode]
public class ShootingSysteme : MonoBehaviour
{
    [SerializeField] private GameObject _shootingTarget;
    [SerializeField] private GameObject _shootingTargetNoAim;
    [SerializeField] private GameObject _shootingTargetAim;
    [SerializeField] private GameObject _shootingOrigin;
    [SerializeField] private float _maxShootDistance = 300;
    [SerializeField] private GameObject _impact;
    [SerializeField] private CompTargetDead _compTargetDead;

    [SerializeField] private Text _maxAmmoText;
    [SerializeField] private Text _ammoText;
    [SerializeField] private Transform _CursorTransf;

    [SerializeField] private CinemachineVirtualCamera CameraV;
    //[SerializeField] private GameObject _ammoPhysique;
    
    public bool _wasShot;
    public int _ammo;
    public int _ammoMax = 12;

    //[SerializeField] private Vector3 _ammoPhysiquePosInit;

    private StarterAssetsInputs _input;
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;
        _ammo = _ammoMax;
    }

    // Update is called once per frame
  
    void Update()
    {
        Vector3 shootPoint = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _maxShootDistance));

        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
           
        Physics.Raycast(ray, out RaycastHit hitInfo2, _maxShootDistance);
        _CursorTransf.SetPositionAndRotation(hitInfo2.point, _CursorTransf.rotation);
        
        if (_input.isAiming)
        {
            _shootingTarget.transform.position = _shootingTargetAim.transform.position;

            if (!_wasShot && _input.isShot)
            {
                _wasShot = true;

                if (_input.isShot && _input.isAiming && _ammo > 0)
                {
                    //_shootingTarget.SetActive(true);
                    _shootingOrigin.SetActive(true);
                    Vector3 forw = this.transform.forward;

                    Debug.DrawRay(ray.origin, CameraV.transform.forward * _maxShootDistance, Color.red, 0.5f);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxShootDistance))
                    {
                        Debug.Log(hitInfo.collider.gameObject.name);

                        //_shootingTarget.transform.position = hitInfo.point;
                        //if (_input.isShot)
                        if (hitInfo.collider.gameObject.tag != "Shoot")
                        {
                            Instantiate(_impact, hitInfo.point, Quaternion.identity);
                        }
                        
                    }

                    _ammo -= 1;
                }
                else
                {
                    //_shootingTarget.SetActive(false);
                    _shootingOrigin.SetActive(false);
                    //_shootingTarget.transform.position = _originCamera;

                    //_ammoPhysique.transform.position = _ammoPhysiquePosInit;
                }
            }
            else if (_wasShot && !_input.isShot)
            {
                _wasShot = false;
                //_shootingTarget.SetActive(false);
                _shootingOrigin.SetActive(false);
            }
        }
        else
        {
            _shootingTarget.transform.position = _shootingTargetNoAim.transform.position;
        }

        if (_input.isReaload && _ammo <= 11)
        {
            _ammo = _ammoMax;
        }
        
        _compTargetDead.StringTargetLife();

        _ammoText.text = _ammo.ToString();
        _maxAmmoText.text = _ammoMax.ToString();
    }
}