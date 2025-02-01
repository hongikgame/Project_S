using System;
using UnityEngine;
using UnityEngine.Playables;

public class OceanTestBehaviour : PlayableBehaviour
{
    
    public bool playSplashSound;
    public bool playEntryAnimation;
    public bool enablePlayerSwim;

    private bool triggered = false;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!triggered)
        {
            Debug.Log("바닷속 입장");

            if (playEntryAnimation)
            {
                PlayableDirector cutscene = GameObject.Find("WaterEntryCutscene")?.GetComponent<PlayableDirector>();
                if (cutscene != null)
                {
                    Debug.Log("바닷속 입장 애니메이션");
                    cutscene.Play();
                }
            }

            // 2. 효과음 재생 (설정값에 따라 실행 여부 결정)
            if (playSplashSound)
            {
                AudioSource waterSound = GameObject.Find("WaterSplashSound")?.GetComponent<AudioSource>();
                if (waterSound != null)
                {
                    Debug.Log("물에 빠지는 효과음 재생");
                    waterSound.Play();
                }
            }

            triggered = true;
        }
    }
}
