using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    public string labelText = "Collect all 5 items and win your freedom!";
    public int maxItems = 5;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    private int _itemsCollected = 0;
    public int Items {
        get {return _itemsCollected;}
        set {
            _itemsCollected = value;
            if(_itemsCollected >= maxItems) {
                labelText = "You've found all the items!";
                showWinScreen = true;

                Time.timeScale = 0f;
            } else {
                labelText = "item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
            Debug.LogFormat("Items: {0}", _itemsCollected);
        }
    }

    private int _playerHP = 10;
    public int HP {
        get {return _playerHP;}
        set {
            _playerHP = value;

            if(_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got to hurt.";
            }
            Debug.LogFormat("Health: {0}", _playerHP);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
    }

    void OnGUI() {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items:" + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 30), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 -100, Screen.height / 2 - 50, 200, 100), "You've collected all the Items!"))
            {
                Utilities.RestartLevel(0);
            }
        }

        if(showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 -100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
