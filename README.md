# DNETFW-2-DNETCORE
This repo is an example of using MachineKey.Protect in a 4.8 built project to see if the value can be Unprotected in a .NET 6 Console Application for testing reasons

Instructions to recreate issues found:

1. The default folder is for the keys is C:\test\myapp-keys\.  If you want to change it update the code in consoleapp1/program.cs & WebApplication1/DataProtectionDemo.cs.
2. Run the WebApplication project and copy the decrypted value that it produces. 
3. Run the ConsoleApp1 project and paste that decrypted value into it.  

Currently #3 is failing, and it shouldn't fail.  It should decrypt the value correctly, which currently should be "TEST VALUE". 
