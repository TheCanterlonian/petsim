//petsim2 by Tiffany Erika Darling (that's right, legal name change motherfuckers!)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Xml;
using System.Runtime.InteropServices;
using Gtk;
using Xamarin; //using Xamarin.Forms; is more useful but conflicts with GTK (how do i pronounce Xamarin btw? i've been saying /xæ mɐ ɾɪn/ myself)
/*
digital pet simulator 2 (1 lost a lot of work so i decided to rewrite it all from scratch)
*/
namespace petsim2
{
    //class for iceberg tip processing
    public class Program
    {
        //program entry point
        public static void Main(string[] args)
        {
            //for every argument
            for (int i = 0; i < args.Length; i++)
            {
                //make the argument lowercase
                args[i] = args[i].ToLower();
            }
            //see if the user wants to start the CLI instantly
            if (args.Contains("c"))
            {
                //start the CLI now
            }
            //see if user wants to start the GUI instantly
            if (args.Contains("g"))
            {
                //preload (check what OS we're working on)
                //start the GUI now
            }
            /*
            testing stuff, (change this for release,) put tests below here
            */
            string os = petsimGraphicalTools.PreGraphicalOperations.OperatingSystemChecker();
            Console.WriteLine(os);
        }
    }
    /*
    The classes below contain primitive methods for handling simple tasks.
    There are things that need to be created that have not yet been created listed here in order of importance.
    The functions still yet to be implemented are as follows:
    0. a data return function to read data from a file of defaults and return the set asked for (for new profile data creation)
    1. a method for finding out if a string is one of a group of strings to replace inline illegal filename checks but also for more general tasks too
    2. a method for creating new save files
    3. a method for loading save files
    4. perfect the console menu creator (petsimConsoleTools.ConsoleOutputGiving.menuCreator(string[]);) it currently ASSUMES element 0 exists
    5. a method for loading save data
    6. a class for reading data from the filesystem and the internet (including default data from default filles)
    7. a class for runtime stuff
    8. a class for console interaction (high level in & out)
    9. a function to check what OS we are running on and decide which version of the GUI to run
    10. a gui for each: linux, windows, and mobile
    11. a background processor for the gui
    12. credits/about box (both console and gui readable)
    13. allow the program to take arguments (to activate the GUI or CLI intantly)
    14. async stuff to run the game while the interface is open
    */
}
/*
The classes below are tools and utilities for all interfaces.
*/
namespace petsimGeneralTools
{
    //class for tools used to keep track of and used for processing the game state
    public class GameStateTools
    {
        //profile loader
        public static bool profileAccessor(string filenameToLoadDataFrom)
        {
            //if file is empty
            if(File.ReadAllText(filenameToLoadDataFrom) == (""))
            {
                //create new data
                petsimGeneralTools.FilesystemEditingAndAltering.newProfileCreator(filenameToLoadDataFrom);
            }
            //otherwise
            else
            {
                //check if data is valid, if not, return false
            }
            //load data
            //TODO: actually create the loader so data can be saved
            //OHNO: all my code was deleted because i was an idiot, sorry
            //guess i will just start over and rewrite this all from scratch
            return true;
        }
    }
    //class for  high level data processing tools
    public class DataProcessingToolsHigh
    {
        //
    }
    //class for  low level data processing tools
    public class DataProcessingToolsLow
    {
        //
    }
    //class for filesystem editing
    public class FilesystemEditingAndAltering
    {
        //Profile Data Creator
        public static bool newProfileCreator(string fileToCreateDataFor)
        {
            //try to write new data
            try
            {
                //grab the data required for making a new file
                string dataToCreate = StaticReturns.newProfileData();
                //opens the file to editing
                StreamWriter swProfileCreate = new StreamWriter(fileToCreateDataFor);
                //write the new data to the file
                swProfileCreate.Write(dataToCreate);
                //be sure to close the streams
                swProfileCreate.Close();
            }
            //if it fails
            catch
            {
                //return that it didn't complete correctly
                return false;
            }
            //if it doesn't fail, return that it completed correctly
            return true;
        }
        //file deleter (returns true if successful)
        public static bool fileDeleter(string chosenFileForDeleting)
        {
            //check for illegal files
            if ((((chosenFileForDeleting.Contains("error")) || (chosenFileForDeleting.Contains("petsim"))) || ((chosenFileForDeleting.Contains("init")) || (chosenFileForDeleting.Contains("cache")))) || (((chosenFileForDeleting.Contains("template")) || (chosenFileForDeleting.Contains("vscode"))) || ((chosenFileForDeleting.Contains("cs")) || (chosenFileForDeleting.Contains(" ")))))
            {
                //don't delete illegal files
                return false;
            }
            //try to delete the file
            try
            {
                //deletion
                File.Delete(chosenFileForDeleting);
            }
            //if file can't be deleted
            catch
            {
                //return incomplete deletion
                return false;
            }
            //return deletion completed
            return true;
        }
    }
    //class for functions that only return constants
    public class StaticReturns
    {
        //data for new profile creation
        public static string newProfileData()
        {
            //create data variable
            string newDataToMake = ("error");
            //if template file exists
            if (File.Exists("template.xml"))
            {
                //set variable to the contents of the template file
                newDataToMake = File.ReadAllText("template.xml");
            }
            //if template file doesn't exist
            else
            {
                //set variable to a hardcoded value
                newDataToMake = ("<data>\n    <profile>\n        <playerName>unknown</playerName>\n        <numberOfPets>0</numberOfPets>\n        <pronounSubjective>unknown</pronounSubjective>\n        <pronounObjective>unknown</pronounObjective>\n        <pronounPosessive>unknown</pronounPosessive>\n        <seenIntro>false</seenIntro>\n    </profile>\n</data>\n");
            }
            //return the variable
            return(newDataToMake);
        }
    }
    //class for processing tasks performed asynchronously
    public class AsynchronousProcessingTasks
    {
        /*
        //main async threading method
        public async Task MainAsync()
        {
            //do asynchronous stuff
            await Task.Run();
            //block the async main method from returning until after the application is exited
            await Task.Delay(-1);
        }
        //message receiver activates when a message is recieved
        private async Task asynctaskname(string message)
        {
            //trims the message
            message = message.TrimStart();
            message = message.TrimEnd();
            //do stuff with the message
            await Task.Run();
        }
        */
    }
}
/*
The classes below are tools and utilities for the console interface.
*/
namespace petsimConsoleTools
{
    //class for giving outputs to the console
    public class ConsoleOutputGiving
    {
        //menu creator
        public static void menuCreator(string[] linesForMenu)
        {
            /*
            this method uses element zero of the input as the menu title.
            it assumes element 0 is always used.
            try to fix this later since this is a very very very very very very very very very very BAD IDEA!!!!!!!!!!!!
            */
            //for the length of the first element
            for (int i = 0; i < linesForMenu[0].Length; i++)
            {
                //write the top bar
                Console.Write("=");
            }
            Console.Write("\n");
            Console.WriteLine(linesForMenu[0]);
            //for the length of the first element
            for (int i = 0; i < linesForMenu[0].Length; i++)
            {
                //write the bottom bar
                Console.Write("-");
            }
            Console.Write("\n");
            //for every menu option
            for (int i = 1; i <= linesForMenu[0].Length; i++)
            {
                //write the menu option
                Console.WriteLine(linesForMenu[i]);
            }
            Console.Write("\n");
        }
    }
    //class for grabbing higher level inputs on the console
    public class ConsoleInputGrabbingHigh
    {
        //filename handler
        public static string fileNameInputGetter(bool existing)
        {
            //create bool to keep track of selection status
            bool selected = false;
            //if the file spposedly already exists (for file editing)
            if (existing == true)
            {
                while (!selected)
                {
                    //grab the user's idea of a filename
                    string filenameToEdit = ConsoleInputGrabbingLow.stringInputGetter(true, true);
                    //check to make sure it exists
                    if (File.Exists(filenameToEdit))
                    {
                        //return the filename
                        return filenameToEdit;
                    }
                    //otherwise
                    else
                    {
                        //return error by exiting loop
                        selected = true;
                    }
                }
            }
            //if the file supposedly doesn't exist (for file creation)
            else if (existing == false)
            {
                while (!selected)
                {
                    //grab the user's idea of a filename
                    string filenameToCreate = ConsoleInputGrabbingLow.stringInputGetter(true, true);
                    //check to see if it already exists
                    if (File.Exists(filenameToCreate))
                    {
                        //tell the user
                        Console.WriteLine("File already exists.");
                    }
                    //otherwise
                    else
                    {
                        //check for illegal names
                        if ((((filenameToCreate.Contains("error")) || (filenameToCreate.Contains("petsim"))) || ((filenameToCreate.Contains("init")) || (filenameToCreate.Contains("cache")))) || (((filenameToCreate.Contains("template")) || (filenameToCreate.Contains("vscode"))) || ((filenameToCreate.Contains("cs")) || (filenameToCreate.Contains(" ")))))
                        {
                            //tell the user that this is not allowed
                            Console.WriteLine("Illegal filename.");
                        }
                        else
                        {
                            //return the filename
                            return filenameToCreate;
                        }
                    }
                }
            }
            //exited loop, return error
            return ("error");
        }
    }
    //class for grabbing low level inputs on the console
    public class ConsoleInputGrabbingLow
    {
        //string input handler
        public static string stringInputGetter(bool trimmingEnabled, bool allLowercase)
        {
            //grab the input
            string userStringInput = Console.ReadLine();
            Console.WriteLine("");
            //if trimming is enabled
            if (trimmingEnabled)
            {
                //trim the whitespace before and after the string
                userStringInput = userStringInput.TrimStart();
                userStringInput = userStringInput.TrimEnd();
            }
            //if case correction is enabled
            if (allLowercase)
            {
                //correct the case to lowercase
                userStringInput = userStringInput.ToLower();
            }
            //return the input to where it was called
            return userStringInput;
        }
        //selection handler
        public static int answerHandlerMultipleChoice(int numberOfChoices)
        {
            //create bool to keep track of selection status
            bool selected = false;
            //create integer variable to assign user choice into
            int userChoice = (0);
            //while selection has not yet completed
            while (!selected)
            {
                //ask user for input
                Console.Write("Type the number of the option you wish to select, then press ENTER:");
                //take in the answer and assign it to a string variable
                string userEntry = Console.ReadLine();
                Console.WriteLine("");
                //try to parse the string into the int
                bool result = int.TryParse(userEntry, out userChoice);
                //if the parsing fails
                if (result == false)
                {
                    //check if the entry was null
                    if ((userEntry == "") || (userEntry == null))
                    {
                        //tell the user they fucked up
                        Console.WriteLine("Must NOT be null.");
                    }
                    //if not
                    else
                    {
                        //tell the user they fucked up
                        Console.WriteLine("Invalid option.");
                    }
                }
                //otherwise
                else
                {
                    //check if user choice is out of range
                    if ((userChoice < 0) || (userChoice > numberOfChoices))
                    {
                        //tell the user they fucked up
                        Console.WriteLine("Out of range.");
                    }
                    //otherwise
                    else
                    {
                        //check if the choice is not zero
                        if (!(userChoice == 0))
                        {
                            //little joke
                            if (userChoice == 69)
                            {
                                Console.WriteLine("Nice!");
                            }
                            //set selection to be finished
                            selected = true;
                        }
                        //otherwise
                        else
                        {
                            //tell the user they fucked up
                            Console.WriteLine("Not an option.");
                        }
                    }
                }
            }
            //return the choice of the user
            return userChoice;
        }
        //yes or no handler
        public static bool answerHandlerYesOrNo()
        {
            while (true)
            {
                //ask the user for input
                Console.Write("Y/N:");
                //take in the answer and assign it into a single-character variable
                ConsoleKeyInfo userEntry = Console.ReadKey();
                Console.WriteLine("");
                //check to see if the answer is a no
                if ((userEntry.KeyChar == 'n') || (userEntry.KeyChar == 'N'))
                {
                    return false;
                }
                //check to see if the answer is a yes
                if ((userEntry.KeyChar == 'y') || (userEntry.KeyChar == 'Y'))
                {
                    return true;
                }
                //if neither is chosen
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }
    }
}
/*
The classes below are tools and utilities for the graphical interface.
*/
namespace petsimGraphicalTools
{
    //TODO: make a GUI
    //class for functions used before running the GUI (used as preparations)
    public class PreGraphicalOperations
    {
        //OS checker
        public static string OperatingSystemChecker()
        {
            //check for Linux
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return("Linux");
            }
            //check for Windows
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return("Windows");
            }
            //check for BSD
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                return("FreeBSD");
            }
            //check for MacOS
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return("MacOSX");
            }
            //if running on none of those
            else
            {
                //return mobile
                return("mobile");
            }
        }
    }
    //class for tools relating to the use of GTK (for Linux users)
    public class petsimGTKtools
    {
        //
    }
    //class for the runtime relating to the use of GTK (for Linux users)
    public class petsimGTKruntime
    {
        //GTK GUI window startup method
        public static void petsimGTKstartup()
        {
            //initialization
            Application.Init();
            //set window properties
            Window mainWindow = new Window("petsim2 ");
            mainWindow.Resize(200,200);
            //set a label
            Label myLabel = new Label();
            myLabel.Text = "test";
            //Add the label to the form
            mainWindow.Add(myLabel);
            //show what we have created
            mainWindow.ShowAll();
            //run the application
            Application.Run();
        }
    }
    //class for handlers of GTK events (for Linux users)
    public class petsimGTKeventHandlers
    {
        //
    }
}
