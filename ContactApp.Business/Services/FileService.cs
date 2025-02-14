﻿using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using System.Text.Json;

namespace ContactApp.Business.Services;

public class FileService : IFileService
{
    private readonly String _directoryPath;
    private readonly String _filePath;

    public FileService(string directoryPath = "c:\\ProjectsVs\\ContactApp\\Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }

    public void SaveToFile(List<Contact> listIn)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }


            var json = JsonSerializer.Serialize(listIn, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An erro occured while saving the file: {ex.Message}");
        }
    }

    public List<Contact> LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File Not Found.");
                return [];
            }

            var json = File.ReadAllText(_filePath);
            var contactList = JsonSerializer.Deserialize<List<Contact>>(json) ?? [];

            if (contactList.Count == 0)
            {
                Console.WriteLine("No contacts available.");
            }

            return contactList;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Was not able to load the file correctly {ex.Message}");
            return [];
        }
    }
}



