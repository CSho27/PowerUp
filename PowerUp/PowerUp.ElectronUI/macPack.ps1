Set-Location ./electron;
npm install;
npm run build;
Set-Location ..;
electronize build /target osx;
Copy-Item "./Data/data" "./bin/Desktop/osx/bin/Data/data" -recurse;
Remove-Item "./bin/Desktop/osx/bin/appsettings.json";
Rename-Item -Path "./bin/Desktop/osx/bin/appsettings.Release.json" -NewName "appsettings.json"