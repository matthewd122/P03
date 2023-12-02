using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    // Player object to save stats from
    [SerializeField] private PlayerController _toSave;

    //  SAVE FUNCTION - CALL ON ANY EVENT YOU WOULD WANT TO SAVE THE GAME - A SAVE TRIGGER IN THIS PARTICULAR CASE
    public void SaveGame()
    {

        // places the new variables into player prefs
        PlayerPrefs.SetString("Name", _toSave.Name);
        PlayerPrefs.SetInt("Health", _toSave.Health);
        PlayerPrefs.SetInt("Money", _toSave.Money);
        PlayerPrefs.SetInt("Strength", _toSave.Strength);
        PlayerPrefs.SetInt("Dexterity", _toSave.Dexterity);
        PlayerPrefs.SetInt("Intelligence", _toSave.Intelligence);
        PlayerPrefs.SetInt("Luck", _toSave.Luck);
        PlayerPrefs.SetFloat("Move", _toSave.MoveSpeed);
        PlayerPrefs.SetInt("Attack", _toSave.AttackSpeed);

        // saving player's location & rotation to player prefs
        PlayerPrefs.SetFloat("xPos", _toSave.transform.position.x);
        PlayerPrefs.SetFloat("yPos", _toSave.transform.position.y);
        PlayerPrefs.SetFloat("zPos", _toSave.transform.position.z);
        PlayerPrefs.SetFloat("xRot", _toSave.transform.eulerAngles.x);
        PlayerPrefs.SetFloat("yRot", _toSave.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("zRot", _toSave.transform.eulerAngles.z);
    }
}