// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;

public class Transfer
{
    public Transfer(int threshold, int low_fee, int high_fee)
    {
        this.threshold = threshold;
        this.low_fee = low_fee;
        this.high_fee = high_fee;
    }
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }
}

public class Confirmation
{
    public Confirmation(string en, string id)
    {
        this.en = en;
        this.id = id;
    }

    public string en { get; set; }
    public string id { get; set; }
}

public class BankTransferConfig
{

    public string lang { get; set; }
    public Transfer transfer { get; set; }
    public string[] methods { get; set; }
    public Confirmation confirmation { get; set; }

    public static string configFilepath = @"bank_transfer_config.json";
    public BankTransferConfig config;
    public BankTransferConfig()
    {

    }

    public BankTransferConfig(string lang, Transfer transfer, string[] methods, Confirmation confirmation)
    {
        this.lang = lang;
        this.transfer = transfer;
        this.methods = methods;
        this.confirmation = confirmation;
    }
    public BankTransferConfig Load()
    {
        String configJsonData = File.ReadAllText(configFilepath);
        config = JsonSerializer.Deserialize<BankTransferConfig>(configJsonData);
        return config;

        //if (!File.Exists(configFilepath))
        //{
        //    var defaultConfig = new BankTransferConfig();
        //    {
        //        lang = "en",
        //        transfer = new Transfer
        //        {
        //            threshold = 25000000,
        //            low_fee = 6500,
        //            high_fee = 15000,
        //        },
        //        methods = ["RTO (RealTime)", "SKN", "RTGS", "BI FAST"],
        //        confirmation = new Confirmation
        //        {
        //            en = "yes",
        //            id = "ya"
        //        }
        //    };
        //    return defaultConfig;
        //}

        //string jsonContent = File.ReadAllText(configFilepath);
        //var config = JsonSerializer.Deserialize<BankTransferConfig>(jsonContent);

        //JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        //{
        //    WriteIndented = true,
        //};
        //File.WriteAllText(configFilepath, JsonSerializer.Serialize(config, jsonOptions));
        //return config;
    }

    public void Save() {
        string jsonContent = File.ReadAllText(configFilepath);
        var config = JsonSerializer.Deserialize<BankTransferConfig>(jsonContent);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        File.WriteAllText(configFilepath, JsonSerializer.Serialize(config, jsonOptions));
    }
}


class Program
{
    static void Main(string[] args)
    {
        BankTransferConfig bank = new BankTransferConfig();
        bank.config.Load();

        Console.WriteLine($"Running in {bank.lang}");
        if (bank.lang == "en")
        {
            Console.WriteLine($"Please insert the amount of money to transfer:");
        }
        else if (bank.lang == "id")
        {
            Console.WriteLine($"Masukkan jumlah uang yang akan di-transfer:");
        }
        int amount = Int32.Parse(Console.ReadLine());

        int transferFee = 0;
        if (amount <= bank.transfer.threshold)
        {
            transferFee = bank.transfer.low_fee;
        }
        else if(amount >= bank.transfer.threshold)
        {
            transferFee = bank.transfer.high_fee;
        }

        if (bank.lang == "en")
        {
            Console.WriteLine($"Transfer Fee = {transferFee}.");
            Console.WriteLine($"Total amount = {amount + transferFee}");
        }
        else if (bank.lang == "id")
        {
            Console.WriteLine($"Biaya Transfer = {transferFee}.");
            Console.WriteLine($"Total Biaya = {amount + transferFee}");
        }

        if (bank.lang == "en")
        {
            Console.WriteLine("Select transfer method:");
        }
        else if (bank.lang == "id")
        {
            Console.WriteLine("Pilih metode transfer:");
        }

        for (int i = 0; i < bank.methods.Length; i++)
        {
            Console.WriteLine($"{i+1}. {bank.methods[i]}");
        }

        if (bank.lang == "en")
        {
            Console.WriteLine($"Please type {bank.confirmation.en} to confirm the transaction:");
        }
        else if (bank.lang == "id")
        {
            Console.WriteLine($"Ketik {bank.confirmation.en} untuk mengonfirmasi transaksi:");
        }

        if (bank.lang == "en")
        {
            Console.WriteLine("The transfer is completed");
        }
        else if (bank.lang == "id")
        {
            Console.WriteLine("Proses transfer berhasil");
        }
        else
        {
            if (bank.lang == "en")
            {
                Console.WriteLine(" Transfer is cancelled");
            }
            else if (bank.lang == "id")
            {
                Console.WriteLine("Transfer dibatalkan");
            }
        }

    }
}

