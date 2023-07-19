using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

internal class SettingsWindow : EditorWindow
{
    private static SettingsWindow _activeSettingsWindow;
    private FSettings _settings;
    private string _toEmailList;
    private GUIContent _fileLoggerTypeContent;
    private GUIContent _fileLoggerPeriodContent;

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
        var sb = new StringBuilder();
        foreach (var mail in _settings.fTracerSettings.emailSettings.toEmail)
        {
            sb.Append($"{mail}\n");
        }
        _toEmailList = sb.ToString();
        
        _fileLoggerTypeContent = new GUIContent("File Logger Type", "Select the type of save file for file logger");
        _fileLoggerPeriodContent = new GUIContent("File Logger Period",
            "Select the time period which the file logger create new file");
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
            ParseEmailList();
            _settings.Save();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawTracerSettings()
    {
        GUILayout.Label("----Tracer----", EditorStyles.boldLabel);

        _settings.fTracerSettings.sendEmailActive = EditorGUILayout.BeginToggleGroup("Send E-Mail On Crush", _settings.fTracerSettings.sendEmailActive);
        _settings.fTracerSettings.emailSettings.smtpClientHost = EditorGUILayout.TextField("SMTP Client", _settings.fTracerSettings.emailSettings.smtpClientHost);
        _settings.fTracerSettings.emailSettings.fromEmail = EditorGUILayout.TextField("From E-mail", _settings.fTracerSettings.emailSettings.fromEmail);
        _settings.fTracerSettings.emailSettings.fromPassword = EditorGUILayout.PasswordField("From Password", _settings.fTracerSettings.emailSettings.fromPassword);
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
        _settings.fLoggerSettings.firebaseLoggerActive =
            EditorGUILayout.BeginToggleGroup("Firebase Logger", _settings.fLoggerSettings.firebaseLoggerActive);

        EditorGUILayout.EndToggleGroup();
    }
}