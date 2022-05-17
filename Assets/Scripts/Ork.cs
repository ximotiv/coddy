using UnityEngine;

public class Ork : Virtual
{
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.M)) IssueCry();
    }
    public override void IssueCry()
    {
        Debug.Log("Кричу как орк епт");
    }
}
