Set-Location ./electron;
npm install;
npm run build;
Set-Location ..;
electronize build /target osx;
Copy-Item "./Data/data" "./bin/Desktop/mac/PowerUp.app/Contents/Resources/bin/Data/data" -recurse;
Remove-Item "./bin/Desktop/mac/PowerUp.app/Contents/Resources/bin/appsettings.json";
Rename-Item -Path "./bin/Desktop/mac/PowerUp.app/Contents/Resources/bin/appsettings.Release.json" -NewName "appsettings.json"