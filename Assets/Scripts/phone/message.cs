using UnityEngine;
using System.Collections;

public class message : MonoBehaviour
{
    private bool _do_move = false;
    private float _move_by = 0;

    void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        print(sr.sprite.rect);
        print(sr.sprite.bounds);
        print((float)gameObject.transform.localScale.x);
        print(gameObject.transform.position);
    }

    void Update()
    {
        //print (_move_by);
        if (_move_by > 0.0F)
        {
            float step = 0.01F;
            gameObject.transform.position -= new Vector3(0, step, 0);
            _move_by -= step;
        }
        if( gameObject.transform.position.y < -0.65F )
            Destroy(gameObject);
    }
    
    public void Move(float height)
    {
        print("move" + height);  
        _move_by += height;
        _do_move = true;
    }
}
