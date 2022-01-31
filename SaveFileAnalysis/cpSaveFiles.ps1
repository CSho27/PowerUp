Rename-Item -Path "./pm2maus_before.dat" -NewName "./pm2maus_$(Get-Date -Format "MMddyyyyHHmmss").dat"
Rename-Item -Path "./pm2maus_after.dat" -NewName "./pm2maus_before.dat"
Copy-Item "C:/Users/short/OneDrive/Documents/\Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat" -Destination "./pm2maus_after.dat"
Write-Host "Done." -ForegroundColor Green