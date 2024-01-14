using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 velocity;
    [SerializeField] private BulletImpact b1, b2;
    bool isOn;
    // Start is called before the first frame update
    void Update()
    {
        if (!isOn && !b1 && !b2)
        {
            isOn = true;
            _destroy();
        }
    }
    void _destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }    
    public void setVelocity()
    {
        if (b1)
            b1.velocity = velocity; 
        if (b2)
            b2.velocity = velocity; 
    }
}
