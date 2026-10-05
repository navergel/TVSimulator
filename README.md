Visual Studio C#

This application was created to test ReflectView players serial control to displays.  As most supported TV are no longer available, the TV reference manuals were used to simulate the required raw data via serial connections to "control" the TV.  Such controls can be: power, input control, volume, mute, aspect ratio to name a few.  The user interface allows the user to simulate controls such as powering off the TV, which would then be expected to be turned back on by the rvplayer via serial commands.

To build and run:

1. Install .NET 6 SDK (if not already installed):
  Download from https://dotnet.microsoft.com/download
2. Build the application:
   cd "c:\Users\nelson.vergel\OneDrive - Creative Realities Inc\Documents\QADevelopment\TV_simulator"
   dotnet build TVSimulator.csproj
3. Run the application
   dotnet run --project TVSimulator.csproj 
4. create single executable for distribution:
   dotnet publish TVSimulator.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

Versioning:
- To update exe version: edit TVSimulator.csproj
- To view exe version: right-click on the .exe file and select properties, go to Details tab

.exe icon
- To update exe icon: edit TVSimulator.csproj: 
   <ApplicationIcon>app.ico</ApplicationIcon>  (icon should be in same folder)

The executable will be created in bin\Release\net6.0-windows\win-x64\publish\ and can run on any Windows machine without requiring .NET installation.


F5 starts debug mode in VisualCode