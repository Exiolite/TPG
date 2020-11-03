//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FloatingText : MonoBehaviour
{
    private void Update()
    {
        if (GetComponent<Animation>().isPlaying == false)
            Destroy(gameObject);
    }

    public void showText(Transform parent, float value, int colorIndex)
    {
        Color textColor = GameManager.instance.damageColor[colorIndex];

        var go = Instantiate(this, new Vector3(parent.transform.position.x + Random.Range(0, 5), parent.transform.position.y + 5, parent.transform.position.z + Random.Range(0, 5)), Quaternion.Euler(60, 45, 0));
        go.GetComponent<TextMesh>().text = value.ToString();
        go.GetComponent<TextMesh>().color = textColor;
    }
}
