# f-tracer
A stack tracer and logger for unity game engine

### Configuration
To configure F-Tracer settings, go to FTracer>Settings window from topbar. 
![image](https://github.com/Fatihprlg/f-tracer/assets/58040833/27ca4803-7c46-4b9d-9b68-c846aeeed032)

![image](https://github.com/Fatihprlg/f-tracer/assets/58040833/a2efc0dd-1c85-4674-9ffd-c20ff0489055)

### Usage
Import the package in releases or clone repository and get package from Assets/Plugins folder and add it into your project. 

#### Logger
For use logger use FLogger static class and its methods. 
![image](https://github.com/Fatihprlg/f-tracer/assets/58040833/450ee6ed-d58e-49c4-80d5-4a35ce72a4cb)

![image](https://github.com/Fatihprlg/f-tracer/assets/58040833/179803aa-3631-43dd-8055-b766903597c2)

#### Tracer
Tracer automatically initializes itself and calls HandleCrush method when exception occured. If you want call a method when HandleCrush, you can add event to OnExceptionRaised event. 

![image](https://github.com/Fatihprlg/f-tracer/assets/58040833/9a60c8df-8ffc-413c-8994-3d2b42b06cf3)

If you configured mail settings, people thoose in your mail list, will recieve a mail that includes stack trace and last log file. 

![image](https://github.com/Fatihprlg/f-tracer/assets/58040833/19135013-d705-4bb1-ad21-5d4af201614b)

### Dependencies
If you use firebase extensions you will need two packages:
- Firebase Analytics (if use firebase logger)
- Firebase Crushlytics (if use firebase logger)
Otherwise you can delete firebase extensions from project.
