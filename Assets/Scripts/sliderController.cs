using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class sliderController : MonoBehaviour
{
    public Slider uiSlider;
	  
    

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (IsTouchOverSlider(touch))
            {
              Debug.Log("Touch is  on the Slider."); // Use Quaternion.Euler to set the rotation
            }
            else
            {
                Debug.Log("Touch is not on the Slider.");
            }
        }
    }

    bool IsTouchOverSlider(Touch touch)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = touch.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiSlider.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
