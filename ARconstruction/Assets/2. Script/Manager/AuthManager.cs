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
using System;
using Unity.VisualScripting;

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

    [SerializeField]  GameObject signInPanel;
    [SerializeField] GameObject signUpPanel;
    FirebaseUser user;
    FirebaseAuth auth;
    FirebaseFirestore db;

    string displayName;
    string emailAddress;
    string userID;

    string inputSignInID;
    string inputSignInEmail;

    bool isLogIn = false;
    bool isSignUp = false;
    private void Start()
    {
        InitializeFirebase();
    }

    private void OnApplicationQuit()
    {
        UserLogOut();
    }

    void UserLogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
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
               StartCoroutine( UserSignUp());
            }
            else { print("�н����尡 ��ġ���� ����"); }
        }
        else
        {
            print("�̸�ĭ�� �������");
        }
    }
    public void SignInTry()
    {
        GetData(this);
    }
    public IEnumerator UserSignUp()
    {
       var longInTask = auth.CreateUserWithEmailAndPasswordAsync(userEmail.text, userPassword.text).ContinueWith(task =>
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

            AuthResult result = task.Result;
            userID = result.User.UserId;
            SendData(this);
            isSignUp = true;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

        });

        yield return new WaitUntil(() => longInTask.IsCompleted);
        ChangePanel();

    }

    public void ChangePanel()
    {
        if (isSignUp)
        {
            signUpPanel.SetActive(false);
            signInPanel.SetActive(true);
        }
    }

    public IEnumerator SignIn()
    {
        var logInTask = auth.SignInWithEmailAndPasswordAsync(inputSignInEmail, signInPassword.text).ContinueWith(
          task =>
          {
              if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
              {
                  Debug.Log(signInName.text + " �� �α��� �ϼ̽��ϴ�.");
                  isLogIn = true;
              }
              else
              {
                  Debug.Log("�α��ο� �����ϼ̽��ϴ�.");
              }
          });

        yield return new WaitUntil(() => logInTask.IsCompleted);

        if (isLogIn == true)
            SceneManager.LoadScene("ARconstruction");
    }
    private static void SendData(AuthManager instance)
    {
        DocumentReference docRef = instance.db.Collection("users").Document(instance.userName.text/*instance.userID*/);
        UserData userData = new UserData
        {
            UserName = instance.userName.text,
            PassWord = instance.userPassword.text,
            EMail = instance.userEmail.text,
        };
        docRef.SetAsync(userData);
    }
    private static void GetData(AuthManager instance)
    {
        DocumentReference docRef = instance.db.Collection("users").Document(instance.signInName.text);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;
            if (snapshot.Exists)
            {
                UserData userData = snapshot.ConvertTo<UserData>();
                instance.inputSignInID = userData.UserName;
                instance.inputSignInEmail = userData.EMail;

                if (instance.signInName.text == instance.inputSignInID)
                {
                    instance.StartCoroutine(instance.SignIn());
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });

    }

}
[FirestoreData]
public class UserData
{
    [FirestoreProperty] public string UserName { get; set; }
    [FirestoreProperty] public string EMail { get; set; }
    [FirestoreProperty] public string PassWord { get; set; }
}