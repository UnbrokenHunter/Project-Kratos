using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shadow Profile", menuName = "Shadow Profile")]
public class ShadowProfile : ScriptableObject {

    public int offsetY = 0;
    public int offsetX = 0;
    public Color color;

}
