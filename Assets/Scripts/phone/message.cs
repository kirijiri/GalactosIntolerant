using UnityEngine;
using System.Collections;

public class message : MonoBehaviour
{
    private float _move_by = 0;
    private float _spacing = 0.05f;

    void Update()
    {
        if (_move_by > 0.0F)
        {
            float step = 0.05F;
            gameObject.transform.position -= new Vector3(0, step, 0);
            _move_by -= step;
        }

        // remove object when out of scope
        if( gameObject.transform.position.y < -0.65F )
            Destroy(gameObject);
    }
    
    public void Move(float height)
    {
        _move_by += height + _spacing;
    }
}
