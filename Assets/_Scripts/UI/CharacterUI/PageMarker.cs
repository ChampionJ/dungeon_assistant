using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

public class PageMarker: MonoBehaviour
{
    public SpriteColorSetter[] markers;
    private HorizontalScrollSnap scroller;
    // Use this for initialization
    void Start()
    {
        scroller = transform.parent.GetComponentInChildren<HorizontalScrollSnap>();
        scroller.OnSelectionPageChangedEvent.AddListener(OnPageChange);
        scroller.OnSelectionChangeEndEvent.AddListener(OnPageChange);
    }
    public void OnPageChange(int page)
    {
        Debug.Log("Page Changed To: " + page.ToString());
        foreach(SpriteColorSetter scs in markers)
        {
            scs.UpdateType(SpriteType.PageMarker);
        }
        markers[page].UpdateType(SpriteType.PageMarkerActive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
