using Helpers;

while (true)
{
    Console.WriteLine("Select an operation:");
    Console.WriteLine("1. Set Key.");
    Console.WriteLine("2. Set IV.");
    Console.WriteLine("3. Encrypt.");
    Console.WriteLine("4. Decrypt.");
    Console.WriteLine();
    Console.WriteLine("Enter a number:");
    var menuItem = String.Empty;
    var item = 0;
    while (String.IsNullOrEmpty(menuItem))
    {
        menuItem = Console.ReadLine();
        int.TryParse(menuItem, out item);
        if( item == 0 )
            menuItem = String.Empty;
    }

    var value = String.Empty;
    if (item == 1)
    {
        while (String.IsNullOrEmpty(value))
        {
            Console.WriteLine("Enter a value for Key:");
            value = Console.ReadLine();
        }
        EncryptionHelper.SetKey(value);
    }
    else if (item == 2)
    {
        while (String.IsNullOrEmpty(value))
        {
            Console.WriteLine("Enter a value for IV:");
            value = Console.ReadLine();
        }
        EncryptionHelper.SetIV(value);
    }
    else if (item == 3)
    {
        while (String.IsNullOrEmpty(value))
        {
            Console.WriteLine("Enter a value for encryption:");
            value = Console.ReadLine();
        }
        var encryptValue = EncryptionHelper.Encrypt(value);
        Console.WriteLine(encryptValue);
    }
    else if (item == 4)
    {
        while (String.IsNullOrEmpty(value))
        {
            Console.WriteLine("Enter a value for decryption:");
            value = Console.ReadLine();
        }
        var decryptValue = EncryptionHelper.Decrypt(value);
        Console.WriteLine(decryptValue);
    }
    Console.WriteLine();
    Console.ReadKey();
}
