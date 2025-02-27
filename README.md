## I.Reason for creating this topic  
As mentioned in the topic "My-Phishing-Case-2", I encountered a situation where the target had enforced the "DISABLE POWERSHELL.EXE" policy. This prevents any calls to the "powershell.exe" process, making it impossible to execute .ps1 files in the traditional way.However, such policies only block the "powershell.exe" process from starting, everything that PowerShell needs to execute scripts is still present.So, the question is: Can we run PowerShell without launching "powershell.exe"? - Yes, we can. In this topic, I will leverage ".NET Runspace" to bypass the "DISABLE POWERSHELL.EXE" policy and establish a command-and-control channel.  

Oh, besides that, I’m often asked another question: Why am I so stubborn about using PowerShell? - Because we like it. We feel comfortable working with PowerShell. Besides, it's simply too powerful to ignore. Moreover, even in post-exploitation stages after gaining access to a system, finding a way to run PowerShell should be a top priority.  

## II.Into detail  
PowerShell.exe is essentially a GUI (graphical user interface) or CLI (command-line interface) application responsible for handling user input and displaying output. The actual execution of PowerShell commands does not take place within PowerShell.exe but rather in a .NET library called System.Management.Automation.dll - a dynamic-link library(DLL) that manages the core functionalities of PowerShell.When you run PowerShell.exe, it invokes System.Management.Automation.dll to create a runspace. A runspace, in this context, refers to an execution environment where PowerShell commands are executed.  

In summary, the steps involved when a PowerShell command is executed include:  
&nbsp;&nbsp;1.Receive input command ------------> Handled by PowerShell.exe  
&nbsp;&nbsp;2.Create Runspace ------------> Handled by PowerShell.exe  
&nbsp;&nbsp;3.Process Pipeline & Execute command ------------> Handled by System.Management.Automation.dll  

System.Management.Automation.dll is written in C#, so we can use its classes and properties by creating an object from the required class and utilizing the corresponding functionalities.The classes and functions we need to use include:  
<p align="center">
  <img src="https://github.com/user-attachments/assets/b78370d7-3552-4efb-b6a2-c82fa23416fd">
</p>
<p align="center"> 
<span style="font-size:30px;">PowerShell class and AddScript method</span>
</p>  
<p align="center">
  <img src="https://github.com/user-attachments/assets/5fdbfb67-e9f1-4f71-a318-e38e4334ab2f">
</p>
<p align="center"> 
<span style="font-size:30px;">Create method</span>
</p>  
<p align="center">
  <img src="https://github.com/user-attachments/assets/c524d72b-d6e4-45ed-b02d-3976db7f8d9e">
</p>
<p align="center"> 
<span style="font-size:30px;">Invoke method</span>
</p>  

The tasks of these three methods are quite basic: Create() is used to create a new PowerShell instance while also creating a Runspace (by default). AddScript() passes the input command, and Invoke() executes it.Alright, converting it to C#, we will code it similarly as follows:  
<p align="center">
  <img src="https://github.com/user-attachments/assets/0678d083-7ee7-4a48-8edc-58de9a7ba63c">
</p>

Thus, we have met all three conditions to run PowerShell as initially mentioned. Now, we will use this code snippet to execute the PowerShell command and establish a Command-Control channel. To keep it simple, we will follow exactly what was done in "My-phishing-case-1" (or you can make some modifications in terms of content, encrypting style, or bypass techniques).  

All commands invoked (including variable and function declarations) will be retained in the session because we operate within a single Runspace from start to finish.All you need to do is encode the PowerShell payload and assign it to a variable → decode it → pass it into the AddScript function → call Invoke().In this case, if the payload is too long, I recommend splitting the original PowerShell script and concatenating it as a string in C#. Then, add each part to AddScript one by one to debug and avoid AV deleting our .exe if it string too long (for example, McAfee 🤡).  

Oh, a quick note about the AMSI bypass method—during testing, I found that patching the value of AmsiContext tends to be more stable than the previously mentioned AmsiScanBuffer when dealing with security protections. You can use the following command to achieve this:
```
$g=([Ref].Assembly.GetTypes() | ForEach-Object{$_.GetFields('NonPublic,Static') | ForEach-Object {$_ | where-object {$_.Name -like "*iContext"}}}).GetValue($null);
[IntPtr]$ptr=$g;
[Int32[]]$buf=@(0);
$t = "S"+"y"+"s"+"t"+"em"+"."+"R"+"u"+"n"+"t"+"i"+"m"+"e"+"."+"I"+"n"+"t"+"e"+"r"+"o"+"p"+"S"+"e"+"r"+"v"+"i"+"c"+"e"+"s"+"."+"M"+"a"+"r"+"s"+"h"+"a"+"l";
[Type]::GetType($t)::Copy($buf, 0, $ptr, 1);
```
Note: You should invoke the patch before invoking the payload.  

Everything has been provided in the Resource. Below is a chain summarizing everything we have done:
<p align="center">
  <img src="https://github.com/user-attachments/assets/5ffeb23e-5e1b-41ba-8a09-935af73f97cf">
</p>
<p align="center"> 
<span style="font-size:30px;">Full-Chain!</span>
</p>  

## III.Demo  
<p align="center">
  <img src="https://github.com/user-attachments/assets/2db1bd1e-24e1-43b4-a0a9-607839316472">
</p>  

<p align="center">
  <img src="https://github.com/user-attachments/assets/127309bb-d59f-4baf-931f-9a4225a2b334">
</p>  

## IV.References  
I would like to thank the authors of the following articles for providing me with the inspiration to carry out this project:  
+)https://rewterz.com/articles/how-i-bypassed-local-group-policy-and-domain-group-policy-powershell-restrictions  
+)PEN-300: Advanced Penetration Testing Certification  



