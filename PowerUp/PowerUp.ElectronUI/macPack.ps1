Set-Location ./electron;
npm install;
npm run build;
Set-Location ..;
electronize build /target osx;
Copy-Item "./Data/data" "./bin/Desktop/osx-unpacked/resources/bin/Data/data" -recurse;
Remove-Item "./bin/Desktop/osx-unpacked/resources/bin/appsettings.json";
Rename-Item -Path "./bin/Desktop/osx-unpacked/resources/bin/appsettings.Release.json" -NewName "appsettings.json"