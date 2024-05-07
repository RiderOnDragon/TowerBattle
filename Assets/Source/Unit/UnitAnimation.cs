using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    private const int TRACK_INDEX = 0;

    //Skins name
    private const string PLAYER_SIDE_SKIN = "1";
    private const string ENEMY_SIDE_SKIN = "2";
    
    //Animations name
    private const string RUN_ANIM = "run_1";
    private const string ATTACK_ANIM = "attack_1";
    private const string DEATH_ANIM = "death_1";
    private const string IDLE_ANIM = "idle_1";

    public SkeletonAnimation SkeletonAnimation { get => _skeletonAnimation; }

    public void Init(SideEnum side)
    {
        if (side == SideEnum.PLAYER)
            _skeletonAnimation.initialSkinName = PLAYER_SIDE_SKIN;
        else
            _skeletonAnimation.initialSkinName = ENEMY_SIDE_SKIN;

        _skeletonAnimation.Initialize(true);
    }

    public void StartRun()
    {
        _skeletonAnimation.AnimationState.SetAnimation(TRACK_INDEX, RUN_ANIM, true);
    }

    public void StartAttack()
    {
        _skeletonAnimation.AnimationState.SetAnimation(TRACK_INDEX, ATTACK_ANIM, false);
    }

    public void StartDeath()
    {
        _skeletonAnimation.AnimationState.SetAnimation(TRACK_INDEX, DEATH_ANIM, false);
    }

    public void StartIdle()
    {
        _skeletonAnimation.AnimationState.SetAnimation(TRACK_INDEX, IDLE_ANIM, true);
    }
}
