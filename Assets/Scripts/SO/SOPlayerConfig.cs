using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerConfig : ScriptableObject
{
    [Header("Moviment Setup")]
    public float speed;
    public float speedRun;
    public float jumpForce;
    public Vector2 friction = new Vector2(.1f, 0);

    [Header("Animations Setup")]
    public string runBool = "Run";
    public float speedWalkAnim = 1f;
    public float speedRunAnim = 1.5f;
    public float swapDuration = .3f;
    public string jumpBool = "Jump";
    public string landingTrigger = "Landing";
    public string fallBool = "Fall";


    [Header("DOAnimation Jump Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float durationAnimation = 1f;

    [Header("DOAnimation fall Setup")]
    public float fallScaleX = 1.2f;
    public float fallScaleY = .7f;
    public float durationAnimFall = 1f;
    public Ease ease = Ease.OutBack;
}
