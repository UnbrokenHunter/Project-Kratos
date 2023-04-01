using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedShadow : MonoBehaviour {

    public ShadowProfile profile;

    [Header("OR")]

    public int offsetY = 0;
    public int offsetX = 0;
    public Color color;

    public void DrawTextShadow() {
        ShadowChild shadowChild = transform.parent.GetComponentInChildren<ShadowChild>();
        GameObject child;
        if(shadowChild != null) {
            child = shadowChild.gameObject;
            DestroyImmediate(child);
        }
        child = Instantiate(gameObject, Vector3.zero, Quaternion.identity, transform.parent);
        child.transform.SetSiblingIndex(transform.GetSiblingIndex());
        MonoBehaviour[] scriptComponents = child.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in scriptComponents) {
            if (!c.ToString().Contains("RectTransform") && !c.ToString().Contains("CanvasRenderer") && !c.ToString().Contains("UI.Text")) {
                DestroyImmediate(c);
            }
        }
        child.AddComponent<ShadowChild>();
        child.name = "Shadow";
        

        if(profile != null) {
            offsetX = profile.offsetX;
            offsetY = profile.offsetY;
            color = profile.color;
        }

        Vector3 curPos = GetComponent<RectTransform>().anchoredPosition;
        curPos.y += offsetY;
        curPos.x += offsetX;
        child.GetComponent<RectTransform>().anchoredPosition = curPos;
        child.GetComponent<Text>().color = color;
    }
}

[CustomEditor(typeof(AdvancedShadow))]
public class AdvancedShadowEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        AdvancedShadow myScript = (AdvancedShadow)target;
        if(GUILayout.Button("Draw shadow")) {
            myScript.DrawTextShadow();
        }
    }
}
