using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobileUI : MonoBehaviour{

    public static string assetsPath = "Assets/Plugins/MobileUI/prefabs/";

    public static GameObject background = AssetDatabase.LoadAssetAtPath(assetsPath + "back.prefab", typeof(GameObject)) as GameObject;

    public static GameObject button_circle = AssetDatabase.LoadAssetAtPath(assetsPath + "button_circle.prefab", typeof(GameObject)) as GameObject;
    public static GameObject button_l = AssetDatabase.LoadAssetAtPath(assetsPath + "button_l.prefab", typeof(GameObject)) as GameObject;
    public static GameObject button_s = AssetDatabase.LoadAssetAtPath(assetsPath + "button_s.prefab", typeof(GameObject)) as GameObject;
    public static GameObject button_square = AssetDatabase.LoadAssetAtPath(assetsPath + "button_square.prefab", typeof(GameObject)) as GameObject;
    public static GameObject button_xl = AssetDatabase.LoadAssetAtPath(assetsPath + "button_xl.prefab", typeof(GameObject)) as GameObject;
    public static GameObject button_xs = AssetDatabase.LoadAssetAtPath(assetsPath + "button_xs.prefab", typeof(GameObject)) as GameObject;
    public static GameObject button_with_text = AssetDatabase.LoadAssetAtPath(assetsPath + "button_with_text.prefab", typeof(GameObject)) as GameObject;

    public static GameObject gift = AssetDatabase.LoadAssetAtPath(assetsPath + "gift.prefab", typeof(GameObject)) as GameObject;
    public static GameObject scrollbar_type_1 = AssetDatabase.LoadAssetAtPath(assetsPath + "scrollbar_type_1.prefab", typeof(GameObject)) as GameObject;
    public static GameObject scrollbar_type_2 = AssetDatabase.LoadAssetAtPath(assetsPath + "scrollbar_type_2.prefab", typeof(GameObject)) as GameObject;
    public static GameObject search_bar = AssetDatabase.LoadAssetAtPath(assetsPath + "search_bar.prefab", typeof(GameObject)) as GameObject;
    public static GameObject slider = AssetDatabase.LoadAssetAtPath(assetsPath + "slider.prefab", typeof(GameObject)) as GameObject;
    public static GameObject switchOBJ = AssetDatabase.LoadAssetAtPath(assetsPath + "switch.prefab", typeof(GameObject)) as GameObject;
    public static GameObject toggle_type_1 = AssetDatabase.LoadAssetAtPath(assetsPath + "toggle_type_1.prefab", typeof(GameObject)) as GameObject;
    public static GameObject toggle_type_2 = AssetDatabase.LoadAssetAtPath(assetsPath + "toggle_type_2.prefab", typeof(GameObject)) as GameObject;

    public static GameObject icon = AssetDatabase.LoadAssetAtPath(assetsPath + "icon.prefab", typeof(GameObject)) as GameObject;


    [MenuItem("GameObject/Mobile UI/Buttons/Circle", false, 0)] static void Add1(MenuCommand menuCommand) {CreateElement(button_circle, menuCommand);}
    [MenuItem("GameObject/Mobile UI/Buttons/Square", false, 0)] static void Add2(MenuCommand menuCommand) {CreateElement(button_square, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Buttons/Extra Large", false, 0)] static void Add3(MenuCommand menuCommand) {CreateElement(button_xl, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Buttons/Large", false, 0)] static void Add4(MenuCommand menuCommand) {CreateElement(button_l, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Buttons/Small", false, 0)] static void Add5(MenuCommand menuCommand) {CreateElement(button_s, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Buttons/Extra Small", false, 0)] static void Add6(MenuCommand menuCommand) {CreateElement(button_xs, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Buttons/With text", false, 0)] static void Add7(MenuCommand menuCommand) {CreateElement(button_with_text, menuCommand); }

    [MenuItem("GameObject/Mobile UI/Gift", false, 0)] static void Add8(MenuCommand menuCommand) {CreateElement(gift, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Scrollbars/Type 1", false, 0)] static void Add9(MenuCommand menuCommand) {CreateElement(scrollbar_type_1, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Scrollbars/Type 2", false, 0)] static void Add10(MenuCommand menuCommand) {CreateElement(scrollbar_type_2, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Search bar", false, 0)] static void Add11(MenuCommand menuCommand) {CreateElement(search_bar, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Slider", false, 0)] static void Add12(MenuCommand menuCommand) {CreateElement(slider, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Switch", false, 0)] static void Add13(MenuCommand menuCommand) {CreateElement(switchOBJ, menuCommand); }

    [MenuItem("GameObject/Mobile UI/Toggles/Type 1", false, 0)] static void Add14(MenuCommand menuCommand) {CreateElement(toggle_type_1, menuCommand); }
    [MenuItem("GameObject/Mobile UI/Toggles/Type 2", false, 0)] static void Add15(MenuCommand menuCommand) {CreateElement(toggle_type_2, menuCommand); }



    public static void CreateElement(GameObject gameObject, MenuCommand menuCommand) {
        GameObject go = Instantiate(gameObject) as GameObject;
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        go.name = go.name.Split('(')[0];
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
