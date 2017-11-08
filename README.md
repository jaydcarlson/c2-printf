![Screenshot](http://i.imgur.com/pXd8lT2.png)

C2-PRINTF is a small utility that lets you do printf()-style debugging over a C2 debugger connection, for use with the Silicon Labs EFM8 and C8051F microcontrollers.

Full write up available on my blog:
[https://jaycarlson.net/2017/07/16/printf-style-trace-messages-using-c2/](https://jaycarlson.net/2017/07/16/printf-style-trace-messages-using-c2/)

If you build the WPF application you will need to ensure that the following DLLs are in the same folder as the c2-printf executable. 
(i.e. ..\c2-printf\bin\Debug)

- libgcc_s_dw2-1.dll
- libstdc++-6.dll
- slab8051.dll
- slabhiddevice.dll

These can be found in the C:\SiliconLabs\SimplicityStudio\v3\common\tcf and C:\SiliconLabs\SimplicityStudio\v3\common\tcf\plugins folder.