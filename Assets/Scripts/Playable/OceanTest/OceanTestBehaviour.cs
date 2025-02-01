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
            Debug.Log("�ٴ�� ����");

            if (playEntryAnimation)
            {
                PlayableDirector cutscene = GameObject.Find("WaterEntryCutscene")?.GetComponent<PlayableDirector>();
                if (cutscene != null)
                {
                    Debug.Log("�ٴ�� ���� �ִϸ��̼�");
                    cutscene.Play();
                }
            }

            // 2. ȿ���� ��� (�������� ���� ���� ���� ����)
            if (playSplashSound)
            {
                AudioSource waterSound = GameObject.Find("WaterSplashSound")?.GetComponent<AudioSource>();
                if (waterSound != null)
                {
                    Debug.Log("���� ������ ȿ���� ���");
                    waterSound.Play();
                }
            }

            triggered = true;
        }
    }
}
