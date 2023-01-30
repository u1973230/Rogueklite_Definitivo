using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
{
    public Color color = Color.black;
    public float thickness = 1.0f;

    void Start()
    {
        Image img = GetComponent<Image>();
        Material mat = img.material;
        mat.SetFloat("_Outline", thickness);
        mat.SetColor("_OutlineColor", color);
    }
}