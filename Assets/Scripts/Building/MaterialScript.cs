using System.Collections.Generic;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> touching;
    SpriteRenderer sr;
    private bool flashing;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (flashing)
        {
            float color = 2 * Time.deltaTime;

            sr.color = new Color(sr.color.r, sr.color.g + color, sr.color.b + color);

            if (sr.color.g >= 255)
                flashing = false;
        }
    }

    public bool touchingObject()
    {
        if (touching.Count == 0)
            return false;
        else
            return true;
    }

    public void flash()
    {
        sr.color = Color.red;
        flashing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (touching.Count == 0)
            return;
        if (touching.Contains(collision.gameObject))
            touching.Remove(collision.gameObject);
    }
}
