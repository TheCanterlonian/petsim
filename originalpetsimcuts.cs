using System;
using System.IO;
//introduction menu
//menu here
//get the user's choice
//if the user wants to open the CLI
namespace formerMain
{
    class Former
    {
        public static void FormerMain()
        {
            //keep CLI open unless stated otherwise
            bool CLIup = true;
            while (CLIup)
            {
                return;
                /*
                //open the petsim CLI menu
                //menu here
                //get the user choice
                //if user chose to load
                {
                    //grab the name of the file to load
                    Console.WriteLine("Type the name of a profile you wish to load, then press ENTER:");
                    string profileNameGivenForLoading = petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(true);
                    //check for error
                    if (profileNameGivenForLoading == "error")
                    {
                        Console.WriteLine("Profile doesn't exist, check your spelling.");
                    }
                    else
                    {
                        //load the profile using the profile access method
                    }
                }
                //if user chose to create
                {
                    //grab the name of the file to create
                    Console.WriteLine("Type the name of a profile you wish to create, then press ENTER:");
                    string profileNameGivenForCreation = petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(false);
                    //create the profile
                    File.Create(profileNameGivenForCreation);
                }
                //if user chose to delete
                {
                    //grab the name of the file to delete
                    Console.WriteLine("Type the name of a profile you wish to delete, then press ENTER:");
                    string profileNameGivenForDeletion = petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(true);
                    //check for error
                    if (profileNameGivenForDeletion == "error")
                    {
                        Console.WriteLine("Profile doesn't exist, check your spelling.");
                    }
                    else
                    {
                        //execute the profile deletion function
                        petsimGeneralTools.FilesystemEditingAndAltering.fileDeleter(profileNameGivenForDeletion);
                    }
                }
                //if user chose to list
                {
                    Console.WriteLine("");
                    //assign the current working directory as the current folder
                    string curentFolder = Directory.GetCurrentDirectory();
                    //grab an array of the files
                    string[] fils = Directory.GetFiles(curentFolder);
                    //enumerate them as a list
                    foreach (string fil in fils)
                    {
                        //print them all one by one
                        Console.WriteLine(Path.GetFileName(fil));
                    }
                }
                */
            }
        }
    }
}