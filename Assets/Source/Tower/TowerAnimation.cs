using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    private const int TRACK_INDEX = 0;

    private const string DEATH_ANIM = "death";

    public SkeletonAnimation SkeletonAnimation { get => _skeletonAnimation; }

    public void StartDeath()
    {
        _skeletonAnimation.AnimationState.SetAnimation(TRACK_INDEX, DEATH_ANIM, false);
    }
}
