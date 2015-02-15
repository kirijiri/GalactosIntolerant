using UnityEngine;
using System.Collections;

public static class mouseInput {

    public static Vector3 GetScreenPosition(){
        Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        pos = Camera.main.ScreenToWorldPoint(pos);
        return Reduce3DTo2D(pos);
    }

    public static Vector3 Reduce3DTo2D(Vector3 vector){
        vector.z = 0;
        return vector;
    }

           

}
