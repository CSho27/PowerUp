Set-Location ./electron;
npm install;
npm run build;
Set-Location ..;
electronize build /target win;
Copy-Item "./Data" "./bin/Desktop/win-unpacked/resources/bin/Data" -recurse;
Remove-Item "./bin/Desktop/win-unpacked/resources/bin/appsettings.json";
Rename-Item -Path "./bin/Desktop/win-unpacked/resources/bin/appsettings.Release.json" -NewName "appsettings.json"