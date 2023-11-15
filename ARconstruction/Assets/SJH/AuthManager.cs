using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using TMPro;
using System.Net.Mail;

public class AuthManager : MonoBehaviour
{
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_InputField userEmail;
    [SerializeField] TMP_InputField userPassword;
    [SerializeField] TMP_InputField confirmPassword;

    FirebaseUser user;
    FirebaseAuth auth;

    string displayName;
    string emailAddress;


    private void Start()
    {
        InitializeFirebase();
    }
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    public void SignUpTry()
    {
        if (userName != null)
        {
            if (userPassword.text == confirmPassword.text) { UserSignUp(); }
            else { print("패스워드가 일치하지 않음"); }
        }
        else
        {
            print("이름칸이 비어있음");
        }
    }
    void UserSignUp()
    {
        auth.CreateUserWithEmailAndPasswordAsync(userEmail.text, userPassword.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }


}
