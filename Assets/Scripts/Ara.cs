using UnityEngine;

public class Ara : Virtual
{
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.N)) IssueCry();
    }
}
