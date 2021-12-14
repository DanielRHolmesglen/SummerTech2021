using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerExample : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    ParticleSystem particles;
    Vector3 startSize;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        startSize = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    { 

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = startSize * 2;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = startSize;
    }
   
}
