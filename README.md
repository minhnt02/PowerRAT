## I.Reason for creating this topic  
As mentioned in the topic "My-Phishing-Case-2", I encountered a situation where the target had enforced the "DISABLE POWERSHELL.EXE" policy. This prevents any calls to the "powershell.exe" process, making it impossible to execute .ps1 files in the traditional way.However, such policies only block the "powershell.exe" process from starting, everything that PowerShell needs to execute scripts is still present.So, the question is: Can we run PowerShell without launching "powershell.exe"? - Yes, we can. In this topic, I will leverage ".NET Runspace" to bypass the "DISABLE POWERSHELL.EXE" policy and establish a command-and-control channel.  

Oh, besides that, I’m often asked another question: Why am I so stubborn about using PowerShell? - Because we like it. We feel comfortable working with PowerShell. Besides, it's simply too powerful to ignore. Moreover, even in post-exploitation stages after gaining access to a system, finding a way to run PowerShell should be a top priority.  
## II.Some additional explanations  
1.What is AMSI and how to defeat it ?  
2.What is .NET Runspace and how to leverage it?  
## III.Into detail  


<span style="font-size:100px;">I Still Working on it, detailed analysis will be available soon!</span>

![image](https://github.com/user-attachments/assets/5ffeb23e-5e1b-41ba-8a09-935af73f97cf)  
<p align="center"> 
<span style="font-size:30px;">Full-Chain!</span>
</p>
## III.Demo
## IV.References
I would like to thank the authors of the following articles for providing me with the inspiration to carry out this project:
+)https://rewterz.com/articles/how-i-bypassed-local-group-policy-and-domain-group-policy-powershell-restrictions  
+)PEN-300: Advanced Penetration Testing Certification  



