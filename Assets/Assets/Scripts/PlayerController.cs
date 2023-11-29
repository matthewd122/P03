using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Stats attached to the player character / to be saved when calling the "save" function
    [Header("Player Stats")]
    [SerializeField] private string _name;
    [SerializeField] private int _health;
    [SerializeField] private int _money;
    [SerializeField] [Range(1, 99)] private int _strength;
    [SerializeField] [Range(1, 99)] private int _dexterity;
    [SerializeField] [Range(1, 99)] private int _intelligence;
    [SerializeField] [Range(1, 99)] private int _luck;
    [SerializeField] private float _moveSpeed = 6.0f;
    [SerializeField] private int _attackSpeed;
    // end of savable player character stats


    // character movement variables *NOT REQUIRED FOR SAVE PROCESS*
    private float _jumpHeight = 4;
    private float _gravity = 10;
    private bool _canMove = true;
    private float _lookSpeed = 2;
    private float _lookXLimit = 45;
    private float _rotationX = 0;
    Vector3 _moveDirection = Vector3.zero;
    CharacterController _characterController;
    [Header("Player Camera")]
    public Camera _playerCamera;
    // end of character movement variables

    private void Start()
    {
        // for controller & begin play process 
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // simple character movement script *NOT REQUIRED FOR SAVE PROCESS*
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = _canMove ? _moveSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = _canMove ? _moveSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(Input.GetButton("Jump") && _canMove && _characterController.isGrounded)
        {
            _moveDirection.y = _jumpHeight;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }

        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);

        if (_canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
        }
        // end of character movement
    }

    // script for changine name *NOT REQUIRED FOR SAVE PROCESS / only to change name value from default*
    public void ChangeName()
    {
        if (_name == "Default Name")
        {
            _name = "Not Default Name";
        }
        else
        {
            _name = "Default Name";
        }
    }

    // script for taking damage *NOT REQUIRED FOR SAVE PROCESS / only to change health value from default*
    public void TakeDamage()
    {
        _health -= 10;
    }

    // script for adding money *NOT REQUIRED FOR SAVE PROCESS / only to change money value from default*
    public void AddMoney()
    {
        _money += 5;
    }

    // script for adding strength *NOT REQUIRED FOR SAVE PROCESS / only to change strength value from default*
    public void AddStrength()
    {
        _strength += 1;
    }

    // script for adding dexterity *NOT REQUIRED FOR SAVE PROCESS / only to change dexterity value from default*
    public void AddDexterity()
    {
        _dexterity += 1;
    }

    // script for adding intelligence *NOT REQUIRED FOR SAVE PROCESS / only to change intelligence value from default*
    public void AddIntelligence()
    {
        _intelligence += 1;
    }

    // script for adding luck *NOT REQUIRED FOR SAVE PROCESS / only to change luck value from default*
    public void AddLuck()
    {
        _luck += 1;
    }

    // script for adding move speed *NOT REQUIRED FOR SAVE PROCESS / only to change move speed value from default*
    public void AddMoveSpeed()
    {
        _moveSpeed += 1.0f;
    }

    // script for adding attack speed *NOT REQUIRED FOR SAVE PROCESS / only to change attack speed value from default*
    public void AddAttackSpeed()
    {
        _attackSpeed += 1;
    }
}