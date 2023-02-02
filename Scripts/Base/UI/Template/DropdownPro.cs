using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WeAreProStars.Core.Manage
{
    public class DropdownPro : Dropdown
    {

    }

    public class DropdownProData
    {
        #region selectable vars.
        public bool interactable;
        public Selectable.Transition transition;
        public Graphic targetGraphic;
        public ColorBlock colors;
        public Navigation navigation;
        #endregion

        #region dropdown vars.
        public RectTransform template;
        public Text captionText;
        public Image captionImage;
        public Text itemText;
        public Image itemImage;
        public int value;
        public float alphaFadeSpeed;
        public List<Dropdown.OptionData> options;
        public Dropdown.DropdownEvent onValueChanged;
        #endregion

        public DropdownProData(Dropdown dropdown)
        {
            interactable = dropdown.interactable;
            transition = dropdown.transition;
            targetGraphic = dropdown.targetGraphic;
            colors = dropdown.colors;
            navigation = dropdown.navigation;

            template = dropdown.template;
            captionText = dropdown.captionText;
            captionImage = dropdown.captionImage;
            itemText = dropdown.itemText;
            itemImage = dropdown.itemImage;
            value = dropdown.value;
            alphaFadeSpeed = dropdown.alphaFadeSpeed;
            options = dropdown.options;
            onValueChanged = dropdown.onValueChanged;
        }

        public void ApplyTo(Dropdown dropdown)
        {
            dropdown.interactable = interactable;
            dropdown.transition = transition;
            dropdown.targetGraphic = targetGraphic;
            dropdown.colors = colors;
            dropdown.navigation = navigation;

            dropdown.template = template;
            dropdown.captionText = captionText;
            dropdown.captionImage = captionImage;
            dropdown.itemText = itemText;
            dropdown.itemImage = itemImage;
            dropdown.value = value;
            dropdown.alphaFadeSpeed = alphaFadeSpeed;
            dropdown.options = options;
            dropdown.onValueChanged = onValueChanged;
        }
    }
}
