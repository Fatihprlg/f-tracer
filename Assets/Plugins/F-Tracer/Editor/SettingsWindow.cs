using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

internal class SettingsWindow : EditorWindow
{
    private static SettingsWindow _activeSettingsWindow;
    private FSettings _settings;
    private string _toEmailList;
    private GUIContent _fileLoggerTypeContent;
    private GUIContent _fileLoggerPeriodContent;
    private GUIContent _databaseLoggerConnectionContent;
    private GUIContent _databaseLoggerUNameContent;
    private GUIContent _databaseLoggerPwdContent;

    [MenuItem("FTracer/Settings")]
    private static void ShowWindow()
    {
        if (_activeSettingsWindow is not null) _activeSettingsWindow.Close();
        _activeSettingsWindow = (SettingsWindow)GetWindow(typeof(SettingsWindow));
        _activeSettingsWindow.minSize = new Vector2(400, 600);
        _activeSettingsWindow.Show();
    }

    private void OnEnable()
    {
        _settings = FSettings.Load();

        _fileLoggerTypeContent = new GUIContent("File Logger Type", "Select the type of save file for file logger");
        _fileLoggerPeriodContent = new GUIContent("File Logger Period",
            "Select the time period which the file logger create new file");
        _databaseLoggerConnectionContent = new GUIContent("Connection String", "Enter database connection string");
        _databaseLoggerUNameContent = new GUIContent("Username", "Enter the username for connection");
        _databaseLoggerPwdContent = new GUIContent("Password", "Enter the password for connection");
    }

    private void OnGUI()
    {
        GUILayout.Label("F Tracer Settings", EditorStyles.boldLabel);

        DrawLoggerSettings();
        DrawTracerSettings();
        DrawBottomButtons();
    }

    private void ParseEmailList()
    {
        var emailList = new List<string>();
        using (var sr = new StringReader(_toEmailList))
        {
            while (sr.ReadLine() is { } line)
            {
                Debug.Log(line);
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var regex =
                    new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (regex.IsMatch(line))
                {
                    emailList.Add(line);
                }
            }
        }

        _settings.fTracerSettings.emailSettings.toEmail = emailList.ToArray();
    }

    private void DrawBottomButtons()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset To Default"))
        {
            _settings = new FSettings();
            _settings.Save();
        }

        if (GUILayout.Button("Cancel"))
        {
            _activeSettingsWindow.Close();
        }

        if (GUILayout.Button("Save"))
        {
            if (!_settings.fTracerSettings.customSmtp)
            {
                _settings.fTracerSettings.emailSettings = new FEmailSettings();
            }

            ParseEmailList();
            _settings.Save();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawTracerSettings()
    {
        GUILayout.Label("----Tracer----", EditorStyles.boldLabel);

        _settings.fTracerSettings.sendEmailActive =
            EditorGUILayout.BeginToggleGroup("Send E-Mail On Crush", _settings.fTracerSettings.sendEmailActive);
        _settings.fTracerSettings.customSmtp =
            EditorGUILayout.BeginToggleGroup("Custom SMTP Client (Recommended)", _settings.fTracerSettings.customSmtp);
        _settings.fTracerSettings.emailSettings.smtpClientHost = EditorGUILayout.TextField("SMTP Client", _settings.fTracerSettings.emailSettings.smtpClientHost);
        _settings.fTracerSettings.emailSettings.fromEmail = EditorGUILayout.TextField("From E-mail", _settings.fTracerSettings.emailSettings.fromEmail);
        _settings.fTracerSettings.emailSettings.fromPassword = EditorGUILayout.PasswordField("From Password", _settings.fTracerSettings.emailSettings.fromPassword);
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.PrefixLabel(new GUIContent("To Email List", "One e-mail per line"));
        _toEmailList = EditorGUILayout.TextArea(_toEmailList, GUILayout.MinHeight(200));
        EditorGUILayout.EndToggleGroup();
    }

    private void DrawLoggerSettings()
    {
        GUILayout.Label("----Logger----", EditorStyles.boldLabel);
        _settings.fLoggerSettings.consoleLoggerActive =
            EditorGUILayout.BeginToggleGroup("Console Logger", _settings.fLoggerSettings.consoleLoggerActive);
        EditorGUILayout.EndToggleGroup();
        _settings.fLoggerSettings.fileLoggerActive =
            EditorGUILayout.BeginToggleGroup("FileLogger Logger", _settings.fLoggerSettings.fileLoggerActive);
        _settings.fLoggerSettings.fileLoggerSettings.loggerType =
            (FileLoggerType)EditorGUILayout.EnumPopup(
                _fileLoggerTypeContent,
                _settings.fLoggerSettings.fileLoggerSettings.loggerType);
        _settings.fLoggerSettings.fileLoggerSettings.loggerPeriod =
            (FileLoggerPeriod)EditorGUILayout.EnumPopup(
                _fileLoggerPeriodContent,
                _settings.fLoggerSettings.fileLoggerSettings.loggerPeriod);
        EditorGUILayout.EndToggleGroup();
        _settings.fLoggerSettings.databaseLoggerActive =
            EditorGUILayout.BeginToggleGroup("Database Logger", _settings.fLoggerSettings.databaseLoggerActive);
        _settings.fLoggerSettings.databaseLoggerSettings.connectionString = EditorGUILayout.TextField(
            _databaseLoggerConnectionContent,
            _settings.fLoggerSettings.databaseLoggerSettings.connectionString);
        _settings.fLoggerSettings.databaseLoggerSettings.userName = EditorGUILayout.TextField(
            _databaseLoggerUNameContent,
            _settings.fLoggerSettings.databaseLoggerSettings.userName);
        _settings.fLoggerSettings.databaseLoggerSettings.password = EditorGUILayout.PasswordField(
            _databaseLoggerPwdContent,
            _settings.fLoggerSettings.databaseLoggerSettings.password);
        EditorGUILayout.EndToggleGroup();
        _settings.fLoggerSettings.firebaseLoggerActive =
            EditorGUILayout.BeginToggleGroup("Firebase Logger", _settings.fLoggerSettings.firebaseLoggerActive);

        EditorGUILayout.EndToggleGroup();
    }
}