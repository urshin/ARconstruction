using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    [Header("SignUp Panel")]
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_InputField userEmail;
    [SerializeField] TMP_InputField userPassword;
    [SerializeField] TMP_InputField confirmPassword;

    [Header("SignIn Panel")]
    [SerializeField] TMP_InputField signInName;
    [SerializeField] TMP_InputField signInPassword;


    FirebaseUser user;
    FirebaseAuth auth;
    FirebaseFirestore db;

    string displayName;
    string emailAddress;
    string userID;

    private void Start()
    {
        InitializeFirebase();
    }
    void InitializeFirebase()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
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
            if (userPassword.text == confirmPassword.text) 
            {
                UserSignUp();
            }
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
            userID = result.User.UserId;
            SendData(this);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    private static void SendData(AuthManager instance)
    {
        DocumentReference docRef = instance.db.Collection("users").Document(instance.userID);
        Dictionary<string, object> user = new Dictionary<string, object>
{
        { "Name", instance.userName.text },
        { "Password", instance.userPassword.text },
        { "Email", instance.userEmail.text },
};
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });
    }

    public class UserData
    {
        public string userName;
        public string eMail;
        public string passWord;

        public UserData(string userName, string eMail, string passWord)
        {
            this.userName = userName;
            this.eMail = eMail;
            this.passWord = passWord;
        }
    }

}
