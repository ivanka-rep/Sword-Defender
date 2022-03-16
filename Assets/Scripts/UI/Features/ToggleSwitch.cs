using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI.Features
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleSwitch : MonoBehaviour
    {
        [SerializeField] private Toggle toggle = null;
        
        [Space(10)]
        
        [SerializeField] private Image activeState = null;
        [SerializeField] private Image inactiveState = null;
        
        public void Awake()
        {
            if(!toggle) 
                toggle = gameObject.GetComponent<Toggle>();

            var clearColor = Color.clear;
            var activeColor = activeState.color;
            var inactiveColor = inactiveState.color; inactiveColor.a = 1f;
            
            toggle.onValueChanged.AddListener(value =>
            {
                activeState.color = value ? activeColor : clearColor;
                inactiveState.color = !value ? inactiveColor : clearColor;
            });
        }
    }
}