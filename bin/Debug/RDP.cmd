@echo off
Set pwd=Mwpsl@2011
Set hashtool="C:\Users\msacchetti\Documents\Visual Studio 2008\Projects\Hdmanager\HdManager\bin\Debug\cryptRDP5.exe"
Set outputfile="C:\Users\msacchetti\Documents\Visual Studio 2008\Projects\Hdmanager\HdManager\bin\Debug\Terminal.rdp"
Set comp=10.21.36.74
Set Domain=ASLBILAB-APP2
Aslbinoema.loc
Aslbinoema.loc
Set usr=Noemalife
for /f "tokens=*" %%a in ('%hashtool% %pwd%') do set pwdhash=%%a
Echo screen mode id:i:2>> %outputfile%
Echo desktopwidth:i:1024>> %outputfile%
Echo desktopheight:i:768>> %outputfile%
Echo session bpp:i:24>> %outputfile%
Echo winposstr:s:0,1,32,68,800,572>> %outputfile%
Echo full address:s:%comp%>> %outputfile%
Echo compression:i:1>> %outputfile%
Echo keyboardhook:i:2>> %outputfile%
Echo audiomode:i:2>> %outputfile%
Echo redirectdrives:i:0>> %outputfile%
Echo redirectprinters:i:0>> %outputfile%
Echo redirectcomports:i:0>> %outputfile%
Echo redirectsmartcards:i:1>> %outputfile%
Echo displayconnectionbar:i:1>> %outputfile%
Echo autoreconnection enabled:i:1>> %outputfile%
Echo authentication level:i:0>> %outputfile%
Echo username:s:%usr%>> %outputfile%
Echo domain:s:%domain%>> %outputfile%
Echo alternate shell:s:>> %outputfile%
Echo shell working directory:s:>> %outputfile%
Echo password 51:b:%pwdhash%>> %outputfile%
Echo disable wallpaper:i:1>> %outputfile%
Echo disable full window drag:i:0>> %outputfile%
Echo disable menu anims:i:0>> %outputfile%
Echo disable themes:i:0>> %outputfile%
Echo disable cursor setting:i:0>> %outputfile%
Echo bitmapcachepersistenable:i:1>> %outputfile%
