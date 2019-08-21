using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct controls
{

    public static controls create_control(KeyCode left, KeyCode right,  KeyCode powerup)
    {
        controls control = new controls();
        control.Left = left;
        control.rigth = right;
        control.powerup = powerup;
        return control;
    }

    public KeyCode Left;
    public KeyCode rigth;
    public KeyCode powerup;
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
    public static float PLAYER_SPEED = 200f;
    public static List<controls> playerControls = new List<controls> { controls.create_control(KeyCode.A, KeyCode.D, KeyCode.S), controls.create_control(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow), controls.create_control(KeyCode.B, KeyCode.M, KeyCode.N), controls.create_control(KeyCode.I, KeyCode.O, KeyCode.P) };
    public static List<List<view>> playerViews = new List<List<view>> {
        new List<view> { view.create_view(0, 0, 1, 1) },
        new List<view> { view.create_view(0, 0, 1, 0.5f), view.create_view(0.5f, 0, 1, 0.5f)},
        new List<view> { view.create_view(0, 0, 0.5f, 0.5f), view.create_view(0.5f, 0, 0.5f, 0.5f), view.create_view(0, 0.5f, 0.5f, 1f) },
        new List<view> { view.create_view(0, 0, 0.5f, 0.5f), view.create_view(0.5f, 0, 0.5f, 0.5f), view.create_view(0, 0.5f, 0.5f, 0.5f), view.create_view(0.5f, 0.5f, 0.5f, 0.5f) }
    };
}
