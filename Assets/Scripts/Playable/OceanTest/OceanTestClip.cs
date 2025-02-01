using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class OceanTestClip : PlayableAsset, ITimelineClipAsset
{
    public ClipCaps clipCaps => ClipCaps.None;

    public bool playSplashSound = true;
    public bool playEntryAnimation = true;
    public bool enablePlayerSwim = true;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<OceanTestBehaviour>.Create(graph);
        OceanTestBehaviour behaviour = playable.GetBehaviour();

       
        behaviour.playSplashSound = playSplashSound;
        behaviour.playEntryAnimation = playEntryAnimation;
        behaviour.enablePlayerSwim = enablePlayerSwim;

        return playable;
    }
}
