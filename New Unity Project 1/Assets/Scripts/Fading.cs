using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fading : MonoBehaviour {

    public float fadeInTime;      //the fading speed

    private Image fadePanel;
    private Color currentColor = Color.black;
    
    
    void Start() {
        fadePanel = GetComponent<Image>();
    }

    void Update() {
        if(Time.timeSinceLevelLoad < fadeInTime) {
            //fade in
            float alphaChange = Time.deltaTime / fadeInTime;
            currentColor.a -= alphaChange;
            fadePanel.color = currentColor;
        } else {
            gameObject.SetActive(false);
        }
    }

}
