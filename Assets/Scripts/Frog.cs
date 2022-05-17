using UnityEngine;

public class Frog : Virtual
{
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Y)) IssueCry();
    }
    public override void IssueCry()
    {
        Debug.Log("Кричу как жаба епт");
    }
}
