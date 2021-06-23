using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class TrackVideo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public VideoPlayer video;
    private Slider tracking;
    private bool slideTrack;

    // Start is called before the first frame update
    void Start()
    {
        tracking = this.GetComponent<Slider>();
        slideTrack = false;
    }

    public void OnPointerDown(PointerEventData a)
    {
        slideTrack = true;
    }

    public void OnPointerUp (PointerEventData a)
    {
        float frame = (float)tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
        slideTrack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!slideTrack && video.isPlaying)
        {
            tracking.value = (float)video.frame / (float)video.frameCount;
        }
    }
}
