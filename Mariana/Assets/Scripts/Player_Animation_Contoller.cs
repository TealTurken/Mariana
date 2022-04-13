using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Contoller : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;
    private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The player sprite is missing an Animator Component");
        }

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = true;
        }
        _animator.SetFloat(MovementSpeed, Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
}


