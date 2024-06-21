using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // this script controls the Hover animation effect on character buttons on the menu scene
    
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Hover", false);
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        _animator.SetBool("Hover", true);
    }
    
    public void OnPointerExit(PointerEventData eventdata)
    {
        _animator.SetBool("Hover", false);
    }
}
