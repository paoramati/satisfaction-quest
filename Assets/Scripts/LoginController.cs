﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private string username;
    private string password;
    public bool loginSuccessful;

    InputField inputUsername;
    InputField inputPassword;
    Button btnLogin;
    public Text output;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    void Start()
    {
        //output.text = "";
        username = "";
        password = "";
        loginSuccessful = false;

        inputUsername = UnityUtilities.AssignInputField("inputUsername");
        inputPassword = UnityUtilities.AssignInputField("inputPassword");
        btnLogin = UnityUtilities.AssignButton("btnLogin");

        inputUsername.ActivateInputField();

        btnLogin.onClick.AddListener(() => CheckLogin());

        //DisplayPlayers();

        //DataServiceUtilities.DeleteDatabase();

        //DataServiceUtilities.DeleteDatabase();


        //DataService.DisplayAllLocations();
        //DataService.DisplayAllItems();
        ////DataService.DisplayAllSessions();
        //DataService.DisplayAllSessionItems();

        //Debug.Log(Item.NAME.COMPUTER.ToString());

    }



    private void CheckLogin()
    {
        output.text = "";
        loginSuccessful = false;
        username = inputUsername.text;
        password = inputPassword.text;

        DataService dataService = new DataService();    //could be replaced by a static object

        dataService.Connect();

        if (inputUsername.text != "" && inputPassword.text != "")       //username and password can not be empty
        {
            if (dataService.CheckUsernameExists(username))              //if this username exists already
            {
                if (dataService.CheckLogin(username, password))         //if login is successful
                {
                    loginSuccessful = true;
                }
                else
                {       
                    //inform user that login was not valid
                    inputUsername.ActivateInputField();
                    output.text = "The username or password is invalid";
                }
            }
            else
            {       
                //add new player
                dataService.AddPlayer(username, password);
                loginSuccessful = true;
            }
        }
        else
        {
            output.text = "Username or password can not be empty!";
        }

        if (loginSuccessful)
        {
            GameManager.InitializeGameState(username);
            GameManager.ChangeScene("MenuScene");
        }
    }

    private void ChangeScene()
    {
        GameManager.ChangeScene("MenuScene");
    }
}
