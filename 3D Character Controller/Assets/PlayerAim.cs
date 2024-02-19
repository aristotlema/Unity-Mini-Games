using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//Get RID of Mono Behavior
public class PlayerAim : MonoBehaviour
{
    private Player _player;
    private PlayerControls _controls;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();

        AssignInputEvents();
    }

    public void HandleAim()
    {
        
    }

    private void AssignInputEvents()
    {
        _controls = _player.controls;

        
    }
}
