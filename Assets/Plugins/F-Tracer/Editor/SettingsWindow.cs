using UnityEditor;
using UnityEngine;

internal class SettingsWindow : EditorWindow
{
    private static SettingsWindow _activeSettingsWindow;
    private FSettings _settings;
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
        
        _settings.consoleLoggerActive = EditorGUILayout.BeginToggleGroup("Console Logger", _settings.consoleLoggerActive);
        EditorGUILayout.EndToggleGroup();
        _settings.fileLoggerActive = EditorGUILayout.BeginToggleGroup("FileLogger Logger", _settings.fileLoggerActive);
        _settings.fileLoggerSettings.loggerType =
            (FileLoggerType)EditorGUILayout.EnumPopup(
                _fileLoggerTypeContent,
                _settings.fileLoggerSettings.loggerType);
        _settings.fileLoggerSettings.loggerPeriod =
            (FileLoggerPeriod)EditorGUILayout.EnumPopup(
                _fileLoggerPeriodContent,
                _settings.fileLoggerSettings.loggerPeriod);
        EditorGUILayout.EndToggleGroup();
        _settings.databaseLoggerActive =
            EditorGUILayout.BeginToggleGroup("Database Logger", _settings.databaseLoggerActive);
        _settings.databaseLoggerSettings.connectionString = EditorGUILayout.TextField(_databaseLoggerConnectionContent,
            _settings.databaseLoggerSettings.connectionString);
        _settings.databaseLoggerSettings.userName = EditorGUILayout.TextField(_databaseLoggerUNameContent,
            _settings.databaseLoggerSettings.userName);
        _settings.databaseLoggerSettings.password = EditorGUILayout.PasswordField(_databaseLoggerPwdContent,
            _settings.databaseLoggerSettings.password);
        EditorGUILayout.EndToggleGroup();
        _settings.firebaseLoggerActive =
            EditorGUILayout.BeginToggleGroup("Firebase Logger", _settings.firebaseLoggerActive);

        EditorGUILayout.EndToggleGroup();

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
            _settings.Save();
        }

        EditorGUILayout.EndHorizontal();
    }
}