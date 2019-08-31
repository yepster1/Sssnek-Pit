using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct controls
{

    public static controls create_control(KeyCode left, KeyCode right)
    {
        controls control = new controls();
        control.Left = left;
        control.rigth = right;
        return control;
    }

    public KeyCode Left;
    public KeyCode rigth;
}

public struct view
{

    public static view create_view(float x, float y, float h, float w)
    {
        view view = new view();
        view.x = x;
        view.y = y;
        view.h = h;
        view.w = w;
        return view;
    }

    public float x;
    public float y;
    public float h;
    public float w;


    override
    public String ToString()
    {
        return x + " " + y + " " + h + " " + w;
    }

}

public class Config
{
    public static int MAP_LENGTH = 100;
    public static int MAP_WIDTH = 100;
    public static float PLAYER_SPEED = 10;
    public static float MAX_PLAYER_SPEED = Config.PLAYER_SPEED * 1.5f;
    public static float MIN_PLAYER_SPEED = Config.PLAYER_SPEED * 0.8f;
    public static float PLAYER_ROTATION = 3f;
    public static float PLAYER_JUMP_FORCE = 10.0f; 
    public static float PLAYER_FALL_MULTIPLIER = 2.5f;
    public static float PLAYER_LOW_JUMP_MULTIPLIER = 2f;
    public static List<controls> playerControls = new List<controls> { controls.create_control(KeyCode.A, KeyCode.D), controls.create_control(KeyCode.LeftArrow, KeyCode.RightArrow), controls.create_control(KeyCode.B, KeyCode.N), controls.create_control(KeyCode.O, KeyCode.P) };
    public static List<List<view>> playerViews = new List<List<view>> {
        new List<view> { view.create_view(0, 0, 1, 1) },
        new List<view> { view.create_view(0, 0, 1, 0.5f), view.create_view(0.5f, 0, 1, 0.5f)},
        new List<view> { view.create_view(0, 0, 0.5f, 0.5f), view.create_view(0.5f, 0, 0.5f, 0.5f), view.create_view(0, 0.5f, 0.5f, 1f) },
        new List<view> { view.create_view(0, 0, 0.5f, 0.5f), view.create_view(0.5f, 0, 0.5f, 0.5f), view.create_view(0, 0.5f, 0.5f, 0.5f), view.create_view(0.5f, 0.5f, 0.5f, 0.5f) }
    };
}
