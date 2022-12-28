#define MsBuildOutputDir ".\CruiseDesign\bin\Release\net472"
#define VERSION "2022.12.28"

#define APP "Cruise Design"
#define EXEName "CruiseDesign.exe"
#define SPECIALTAG "Production"
#define BASEURL "https://www.fs.fed.us/forestmanagement/products/measurement"
#define ORGANIZATION "U.S. Forest Service, Forest Management Service Center"

[Setup]
AppName={#APP}
AppMutex={#APP}
AppID={#APP}
AppVersion={#VERSION}
AppVerName={#APP} version {#VERSION}
AppPublisher={#ORGANIZATION}
AppPublisherURL={#BASEURL}
AppSupportURL={#BASEURL}/support.shtml
AppUpdatesURL={#BASEURL}/cruising/index.shtml

DefaultDirName={autopf}\FMSC\{#APP}
DefaultGroupName=FMSC\{#APP}

VersionInfoDescription={#APP} Setup
VersionInfoCompany={#ORGANIZATION}
VersionInfoVersion={#VERSION}

Compression=lzma
SolidCompression=yes

PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog

ChangesEnvironment=yes
ChangesAssociations=yes
AllowUNCPath=no
ShowLanguageDialog=no

OutputBaseFilename=CruiseDesign_Setup_{#VERSION}
OutputManifestFile=Setup-Manifest.txt

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons};

[Files]
Source: "{#MsBuildOutputDir}\*.exe"; DestDir: {app}; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\*.dll"; DestDir: {app}; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\*.exe.config"; DestDir: {app}; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\runtimes\win-x64\native\*.dll"; DestDir: {app}\runtimes\win-x64\native; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\runtimes\win-x86\native\*.dll"; DestDir: {app}\runtimes\win-x86\native; Flags: ignoreversion;
;Source: "..\CruiseDesignBeta.docx"; DestDir: {app}; Flags: ignoreversion


[Icons]
Name: {group}\{#APP}; Filename: {app}\{#EXEName}
Name: {autodesktop}\{#APP}; Filename: {app}\{#EXEName}; Tasks: desktopicon

[Run]
Filename: "{app}\{#EXEName}"; Description: "{cm:LaunchProgram,{#APP}}"; Flags: nowait postinstall skipifsilent

[Registry]
;Register app path
Root: HKA; Subkey: SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{#EXEName}; ValueType: string; ValueData: "{app}\{#EXEName}"; Flags: uninsdeletevalue;  

; Register cruise file
Root: HKA; SubKey: Software\Classes\.design; ValueType: string; ValueData: NCS.DesignFile; Flags: uninsdeletekey
Root: HKA; SubKey: Software\Classes\NCS.DesignFile; ValueType: string; ValueData: Design File; Flags: uninsdeletekey
Root: HKA; SubKey: Software\Classes\NCS.DesignFile\Shell\Open\Command; ValueType: string; ValueData: """{app}\{#EXEName}"" ""%1"""; Flags: uninsdeletevalue
; NEED NEW FILE ICONS Root: HKCR; Subkey: NCS.CruiseFile\DefaultIcon; ValueType: string; ValueData: {app}\crz1.ico; Flags: uninsdeletevalue


; Modify OpenWithList and FileExts for .design files
Root: HKA; SubKey: Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.design; ValueName: Application; ValueType: string; ValueData: " "; Flags: uninsdeletevalue
Root: HKA; SubKey: Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.design\OpenWithList; ValueName: a; ValueType: string; ValueData: {#EXEName}; Flags: uninsdeletevalue
Root: HKA; SubKey: Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.design\OpenWithList; ValueName: b; ValueType: string; ValueData: " "; Flags: uninsdeletevalue
Root: HKA; SubKey: Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.design\OpenWithList; ValueName: c; ValueType: string; ValueData: " "; Flags: uninsdeletevalue
Root: HKA; SubKey: Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.design\OpenWithList; ValueName: MRUList ValueType: string; ValueData: a; Flags: uninsdeletevalue

