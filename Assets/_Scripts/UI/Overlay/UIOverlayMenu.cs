using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlayMenu : MonoBehaviour {
    public virtual OverlayMenuType menuType
    {
        get { return OverlayMenuType.STAT; }
    }
    public virtual void OnShow() { }
}
