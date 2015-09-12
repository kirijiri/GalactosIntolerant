using UnityEngine;
using System.Collections;

public class message : MonoBehaviour
{
    private float _move_by = 0;
    private float _spacing = 0.05f;
	private float step = 1.0F; //0.05F;
	private float i;

    void Update()
    {
        if (_move_by > 0.0F)
        {
			i = step * Time.deltaTime;
            gameObject.transform.position -= new Vector3(0, i, 0);
            _move_by -= i;
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
