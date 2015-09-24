using UnityEngine;
using System.Collections;

public class message : MonoBehaviour
{
    private bool _do_move = false;
    private float _move_by = 0;
    private float step = 1.0F; //0.05F;
    private float i;
    private Vector3 _init_pos = new Vector3(2.266F, 0.11F, 0);
    private float _spacing = 0.05f;

    void Start()
    {
        transform.localPosition = _init_pos;
        transform.localPosition += new Vector3(0, getHeight() + _spacing, 0);
    }

    void Update()
    {
        if (_do_move && _move_by > 0.0F)
        {
            i = step * Time.deltaTime;
            transform.position -= new Vector3(0, i, 0);
            _move_by -= i;
        }
    }
    
    public void Move()
    {
        _do_move = true;
    }

    public bool IsMoving()
    {
        return _do_move;
    }


    public void MoveBy(float height)
    {
        _move_by += height;
    }

    public float getHeight()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        return sr.sprite.bounds.size.y;
    }

    public bool TargetReached()
    {
        return _do_move && (transform.localPosition.y <= _init_pos [1]);
    }

    public bool OutOfBounds()
    {
        return transform.position.y < -0.65F;
    }
}
