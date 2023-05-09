using UnityEngine;

public class GraphicsQuality : MonoBehavior {

  public void SetQuality (int qualityIndex) {
    QualitySettings.SetQualityLevel (qualityIndex);
  }
  
}
