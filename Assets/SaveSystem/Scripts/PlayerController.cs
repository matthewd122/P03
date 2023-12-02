using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // connected save controller
    [SerializeField] private SaveController _controller;

    // Stats attached to the player character / to be saved when calling the "save" function
    [Header("Player Stats")]
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private int _health;
    public int Health => _health;
    [SerializeField] private int _money;
    public int Money => _money;
    [SerializeField] [Range(1, 99)] private int _strength;
    public int Strength => _strength;
    [SerializeField] [Range(1, 99)] private int _dexterity;
    public int Dexterity => _dexterity;
    [SerializeField] [Range(1, 99)] private int _intelligence;
    public int Intelligence => _intelligence;
    [SerializeField] [Range(1, 99)] private int _luck;
    public int Luck => _luck;
    [SerializeField] private float _moveSpeed = 6.0f;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] private int _attackSpeed;
    public int AttackSpeed => _attackSpeed;
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

        // condition to start the player with stats saved in playerprefs if ANY stat is different from the default value
        if (PlayerPrefs.GetString("Name") != null || PlayerPrefs.GetInt("Health") != 100 || PlayerPrefs.GetInt("Money") != 0 || PlayerPrefs.GetInt("Strength") != 1 || PlayerPrefs.GetInt("Dexterity") != 1
            || PlayerPrefs.GetInt("Intelligence") != 1 || PlayerPrefs.GetInt("Luck") != 1 || PlayerPrefs.GetFloat("Move") != 6.0f || PlayerPrefs.GetInt("Attack") != 1)
        {
            // initializes the stat values to the values stored in player prefs
            _name = PlayerPrefs.GetString("Name");
            _health = PlayerPrefs.GetInt("Health");
            _money = PlayerPrefs.GetInt("Money");
            _strength = PlayerPrefs.GetInt("Strength");
            _dexterity = PlayerPrefs.GetInt("Dexterity");
            _intelligence = PlayerPrefs.GetInt("Intelligence");
            _luck = PlayerPrefs.GetInt("Luck");
            _moveSpeed = PlayerPrefs.GetFloat("Move");
            _attackSpeed = PlayerPrefs.GetInt("Attack");

            // initializes player location at the point stored in the player prefs
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"), PlayerPrefs.GetFloat("zPos"));
            gameObject.transform.Rotate(PlayerPrefs.GetFloat("xRot"), PlayerPrefs.GetFloat("yRot"), PlayerPrefs.GetFloat("zRot"));
        }
    }

    private void Update()
    {
        // simple character movement functionality *NOT REQUIRED FOR SAVE PROCESS*
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


        // reset stats on key press *NOT REQUIRED FOR SAVE PROCESS*
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetStats();
        }
    }

    // function for changine name *NOT REQUIRED FOR SAVE PROCESS / only to change name value from default*
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

    // function for taking damage *NOT REQUIRED FOR SAVE PROCESS / only to change health value from default*
    public void TakeDamage()
    {
        _health -= 10;
    }

    // function for adding money *NOT REQUIRED FOR SAVE PROCESS / only to change money value from default*
    public void AddMoney()
    {
        _money += 5;
    }

    // function for adding strength *NOT REQUIRED FOR SAVE PROCESS / only to change strength value from default*
    public void AddStrength()
    {
        _strength += 1;
    }

    // function for adding dexterity *NOT REQUIRED FOR SAVE PROCESS / only to change dexterity value from default*
    public void AddDexterity()
    {
        _dexterity += 1;
    }

    // function for adding intelligence *NOT REQUIRED FOR SAVE PROCESS / only to change intelligence value from default*
    public void AddIntelligence()
    {
        _intelligence += 1;
    }

    // function for adding luck *NOT REQUIRED FOR SAVE PROCESS / only to change luck value from default*
    public void AddLuck()
    {
        _luck += 1;
    }

    // function for adding move speed *NOT REQUIRED FOR SAVE PROCESS / only to change move speed value from default*
    public void AddMoveSpeed()
    {
        _moveSpeed += 1.0f;
    }

    // function for adding attack speed *NOT REQUIRED FOR SAVE PROCESS / only to change attack speed value from default*
    public void AddAttackSpeed()
    {
        _attackSpeed += 1;
    }

    // function to reset stats *NOT REQUIRED FOR SAVE PROCESS*
    private void ResetStats()
    {
        _name = "";
        _health = 100;
        _money = 0;
        _strength = 1;
        _dexterity = 1;
        _intelligence = 1;
        _luck = 1;
        _moveSpeed = 6.0f;
        _attackSpeed = 1;

        PlayerPrefs.SetFloat("xPos", 0);
        PlayerPrefs.SetFloat("zPos", 0);
    }
}