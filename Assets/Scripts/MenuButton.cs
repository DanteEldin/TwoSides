using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    //Variables
    public bool selected = false;
    public int type = 1;
    int selectedType = 1;
    int maxTypes = 2;
    public string nextSceneName;
    public Sprite unselectedSprite;
    public Sprite selectedSprite;

    KeyCode leftKey = KeyCode.A;
    KeyCode rightKey = KeyCode.D;
    KeyCode interactKey = KeyCode.Space;

    //Components
    SpriteRenderer spriteRenderer;

    //Start
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Update
    void Update()
    {
        ButtonSelect();
        ButtonPress();
    }

    //Select
    void ButtonSelect()
    {
        //left
        if (Input.GetKeyDown(leftKey) && !Input.GetKeyDown(rightKey))
        {
            selectedType--;
        }
        //right
        if (!Input.GetKeyDown(leftKey) && Input.GetKeyDown(rightKey))
        {
            selectedType++;
        }

        //overflow
        if (selectedType < 1)
        {
            selectedType = maxTypes;
        }
        else if (selectedType > maxTypes)
        {
            selectedType = 1;
        }

        //check if matching
        if (type == selectedType)
        {
            selected = true;
        }
        else
        {
            selected = false;
        }
    }

    //Press
    void ButtonPress()
    {
        if (selected)
        {
            spriteRenderer.sprite = selectedSprite;
            if (Input.GetKeyDown(interactKey))
            {
                switch (type)
                {
                    //play
                    case 1:
                        SceneManager.LoadSceneAsync(nextSceneName);
                        break;
                    //exit
                    case 2:
                        Application.Quit();
                        break;
                    //default
                    default:
                        Application.Quit();
                        break;
                }
            }
        }
        else
        {
            spriteRenderer.sprite = unselectedSprite;
        }
    }
}
