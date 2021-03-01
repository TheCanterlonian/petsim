using System;
using System.IO;
namespace petsimOriginale
{
    //make this public so it can be called from outside
    public class Program
    {
        //main program starting method is also public
        public static void FormerMain(string[] args)
        {
            //hold console open
            while (true)
            {
                //introduction menu
                Console.WriteLine("");
                Console.WriteLine(" petsim console");
                Console.WriteLine("================");
                Console.WriteLine("   Main Menu");
                Console.WriteLine("----------------");
                Console.WriteLine("");
                Console.WriteLine("1. start GUI");
                Console.WriteLine("2. start CLI");
                Console.WriteLine("3. quit");
                Console.WriteLine("");
                //get the user's choice
                int chosenOption = petsimConsoleTools.ConsoleInputGrabbingLow.answerHandlerMultipleChoice(3);
                //if the user wants to open the GUI
                if (chosenOption == 1)
                {
                    //tell the user that the GUI is loading
                    Console.WriteLine("");
                    Console.WriteLine("GUI doesn't exist yet, please use the CLI until it's completed.");
                    Console.WriteLine("");
                    //start the GUI
                }
                //if the user wants to open the CLI
                else if (chosenOption == 2)
                {
                    //keep CLI open unless stated otherwise
                    bool CLIup = true;
                    while (CLIup)
                    {
                        //open the petsim CLI menu
                        Console.WriteLine("");
                        Console.WriteLine("petsim command line interface");
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("");
                        Console.WriteLine("1. load profile");
                        Console.WriteLine("2. create profile");
                        Console.WriteLine("3. delete profile");
                        Console.WriteLine("4. list files");
                        Console.WriteLine("5. go back to previous menu");
                        Console.WriteLine("");
                        //get the user choice
                        int CLIchoice = petsimConsoleTools.ConsoleInputGrabbingLow.answerHandlerMultipleChoice(5);
                        //if user chose to load
                        if (CLIchoice == 1)
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
                                petsimGeneralTools.GameStateTools.profileAccessor(profileNameGivenForLoading);
                            }
                        }
                        //if user chose to create
                        else if (CLIchoice == 2)
                        {
                            //grab the name of the file to create
                            Console.WriteLine("Type the name of a profile you wish to create, then press ENTER:");
                            string profileNameGivenForCreation = petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(false);
                            //create the profile
                            File.Create(profileNameGivenForCreation);
                        }
                        //if user chose to delete
                        else if (CLIchoice == 3)
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
                        else if (CLIchoice == 4)
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
                        //otherwise
                        else
                        {
                            //exit the loop
                            CLIup = false;
                        }
                    }
                }
                //if user wants to quit
                else
                {
                    //end the program
                    Environment.Exit(0);
                }
            }
            
        }
    }
}

