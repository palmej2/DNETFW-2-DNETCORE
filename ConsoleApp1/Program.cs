using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\test\myapp-keys\"));

var services = serviceCollection.BuildServiceProvider();

var instance = ActivatorUtilities.CreateInstance<Demo>(services);
instance.RunSample();

public class Demo
{
    IDataProtector _protector;

    public Demo(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("my-app");
    }

    public void RunSample()
    {
        Console.Write("Enter Encrypted Value from WebApplication1: ");
        string input = Console.ReadLine();

        string unprotectedPayload = Decrypt(input);
        //EXPECTING this value "TEST VALUE" from HomeController.cs
        Console.WriteLine($"Unprotect returned: {unprotectedPayload}");
        Console.ReadLine();
    }

    public string Encrypt(string text)
    {
        var secureUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
        var encodedText = secureUtf8.GetBytes(text);
        var protectedText = _protector.Protect(encodedText);
        return WebEncoders.Base64UrlEncode(protectedText);
    }

    public string Decrypt(string text)
    {
        var encodedText = WebEncoders.Base64UrlDecode(text);
        var bytes = _protector.Unprotect(encodedText);
        var secureUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
        return secureUtf8.GetString(bytes);
    }
}