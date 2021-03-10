//petsim2 by Tiffany Erika Darling
//the spaghettification of this project is insane!!!
//you have been warned...
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
    //class for early pre-game menus
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
                //start the CLI
                CLImenu();
            }
            //see if user wants to start the GUI instantly
            if (args.Contains("g"))
            {
                //start the GUI
                petsimGraphicalTools.PreGraphicalOperations.graphicalStartup();
            }
            //rootMenu(); //uncomment this on release
            /*
            testing stuff, (change this for release,) put tests below here
            */
            rootMenu();
            return;
        }
        //main menu
        public static void rootMenu()
        {
            while(true)
            {
                //print the main menu and ask get the user to select an option
                petsimConsoleTools.ConsoleOutputGiving.menuCreator(petsimGeneralTools.StaticReturns.stringArrayReturn(1));
                int chosenMenuOption = petsimConsoleTools.ConsoleInputGrabbingLow.answerHandlerMultipleChoice(3);
                //if the user chose to open the GUI
                if (chosenMenuOption == 1)
                {
                    //start the GUI
                    petsimGraphicalTools.PreGraphicalOperations.graphicalStartup();
                }
                //if the user chose to open the CLI
                else if (chosenMenuOption == 2)
                {
                    //start the CLI
                    CLImenu();
                }
                //if the user chose to quit
                else
                {
                    //quit
                    return;
                }
            }
        }
        //command line interface menu
        public static void CLImenu()
        {
            while(true)
            {
                //print CLI menu and wait for answer
                petsimConsoleTools.ConsoleOutputGiving.menuCreator(petsimGeneralTools.StaticReturns.stringArrayReturn(2));
                int chosenMenuOption = petsimConsoleTools.ConsoleInputGrabbingLow.answerHandlerMultipleChoice(5);
                //user choices
                if (chosenMenuOption == 1)
                {
                    //grab the name of the file to load
                    string fileThatWillBeLoaded = petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(true, false);
                    //if there's no error
                    if (!(fileThatWillBeLoaded==("error")))
                    {
                        //load with the profile accessor
                        petsimGeneralTools.GameStateTools.profileAccessor(fileThatWillBeLoaded);
                    }
                    //load save file
                }
                else if (chosenMenuOption == 2)
                {
                    //create the save file
                    petsimGeneralTools.FilesystemEditingAndAltering.newProfileCreator(petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(false, false));
                }
                else if (chosenMenuOption == 3)
                {
                    //grab the filename
                    string fileThatIsToBeDeleted = petsimConsoleTools.ConsoleInputGrabbingHigh.fileNameInputGetter(true, true);
                    //make sure there's no error
                    if (!(fileThatIsToBeDeleted == ("error")))
                    {
                        //delete save file
                        petsimGeneralTools.FilesystemEditingAndAltering.fileDeleter(fileThatIsToBeDeleted);
                    }
                }
                else if (chosenMenuOption == 4)
                {
                    //list files in working directory
                    Console.Write(petsimGeneralTools.FilesystemEditingAndAltering.filesInDirectoryListGetter(Directory.GetCurrentDirectory()));
                }
                else
                {
                    return;
                }
            }
        }
    }
    //class for what
    /*
    The classes below contain primitive methods for handling simple tasks.
    There are things that need to be created that have not yet been created listed here in order of importance.
    The functions still yet to be implemented are as follows:
    0. a data return function to read data from a file of defaults and return the set asked for (for new profile data creation)
    1. a method for loading save files
    2. fix the console menu creator (petsimConsoleTools.ConsoleOutputGiving.menuCreator(string[]);) it currently ASSUMES element 0 exists
    3. a method for loading save data
    4. a class for reading data from the filesystem and the internet (including default data from default filles)
    5. a class for runtime stuff
    6. a class for console interaction (high level in & out)
    7. a function to check what OS we are running on and decide which version of the GUI to run
    8. a background processor for the gui
    9. credits/about box (both console and gui readable)
    10. allow the program to take arguments (to activate the GUI or CLI intantly)
    11. async stuff to run the game while the interface is open
    12. pay a Xamarin developer to make a GUI for me
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
        //profile loader (returns true if successful)
        public static bool profileAccessor(string filenameToLoadDataFrom)
        {
            //assign the save file's content to a string variable
            string saveFileData = File.ReadAllText(filenameToLoadDataFrom);
            //trim the data of whitespace at the beginning and end
            saveFileData = saveFileData.TrimStart();
            saveFileData = saveFileData.TrimEnd();
            //if file is empty
            if(saveFileData == (""))
            {
                //create new data
                bool writeAllNewData = petsimGeneralTools.FilesystemEditingAndAltering.newProfileCreator(filenameToLoadDataFrom);
                //if it was unsuccessful
                if(!(writeAllNewData))
                {
                    //tell the user
                    Console.WriteLine("Could not create save data in this file.");
                    Console.WriteLine("Please check if the file is writable or read-only before trying again.");
                    return false;
                }
            }
            //otherwise
            else
            {
                //create an xml reader for the file that we're loading from
                using var reader = XmlReader.Create(filenameToLoadDataFrom);
                //load primary data
                reader.ReadToFollowing("playerName");
                string playerName = reader.ReadElementContentAsString();
                reader.ReadToFollowing("numberOfPets");
                int numberOfPets = reader.ReadElementContentAsInt();
                //read pronouns in
                reader.ReadToFollowing("pronounSubjective");
                string pronounSubjective = reader.ReadElementContentAsString();
                reader.ReadToFollowing("pronounObjective");
                string pronounObjective = reader.ReadElementContentAsString();
                reader.ReadToFollowing("pronounPosessive");
                string pronounPosessive = reader.ReadElementContentAsString();
                //load save state booleans
                reader.ReadToFollowing("seenIntro");
                bool seenIntro = reader.ReadElementContentAsBoolean();
                reader.ReadToFollowing("eros");
                bool eros = reader.ReadElementContentAsBoolean();
                //
            }
            //TODO: actually create the loader so data can be saved
            //OHNO: all my code was deleted because i was an idiot, sorry
            //guess i will just start over and rewrite this all from scratch
            return false;
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
        //checks if a string is illegal (returns true if illegal)
        public static bool illegalStringChecker(string stringToCheck, int arrayToCheckIn, bool mustBeExactMatch)
        {
            //if the string this is being checked against must exactly match one of the strings in the array to return true
            if(mustBeExactMatch)
            {
                //for every string in the array to check in
                foreach(string stringInTheArray in StaticReturns.stringArrayReturn(0))
                {
                    //if the string to check is the same as one of the strings in the array
                    if (stringToCheck == (stringInTheArray))
                    {
                        //return match found
                        return true;
                    }
                }
                //if the loop exits, it never returned a match
                return false;
            }
            //if the string this is being checked against must contain within it one of the strings in the array to return true
            else
            {
                //for every string in the array to check in
                foreach(string stringInTheArray in StaticReturns.stringArrayReturn(0))
                {
                    //if the string to check contains one of the strings in the array
                    if (stringToCheck.Contains(stringInTheArray))
                    {
                        //return match found
                        return true;
                    }
                }
                //if the loop exits, it never returned a match
                return false;
            }
        }
    }
    //class for filesystem editing
    public class FilesystemEditingAndAltering
    {
        //Profile Data Creator (returns true if successful)
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
        //lists files in given directory
        public static string filesInDirectoryListGetter(string directoryToListFilesIn)
        {
            //create string to facilitate the conversion of an array into a string
            string formerArrayString = ("");
            //grab an array of the files
            string[] filesInDirectory = Directory.GetFiles(directoryToListFilesIn);
            //sort the array
            Array.Sort(filesInDirectory);
            //enumerate them as a list
            foreach (string singleFileInDirectory in filesInDirectory)
            {
                //assign them all one by one from an array into a string
                formerArrayString = (formerArrayString + (Path.GetFileName(singleFileInDirectory)) + ("\n"));
            }
            //return the list of files as a string
            return formerArrayString;
        }
        //file deleter (returns true if successful)
        public static bool fileDeleter(string chosenFileForDeleting)
        {
            //check for illegal files
            if (DataProcessingToolsLow.illegalStringChecker(chosenFileForDeleting, 0, false))
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
                //create it
                File.WriteAllText("template.xml", stringReturn(0));
                //set variable to a hardcoded value
                newDataToMake = stringReturn(0);
            }
            //return the variable
            return(newDataToMake);
        }
        //data for string returning (for long strings)
        public static string stringReturn(int stringToReturn)
        {
            //string returns
            if (stringToReturn == 0)
            {
                return("<data>\n    <profile>\n        <playerName>unknown</playerName>\n        <numberOfPets>0</numberOfPets>\n        <pronounSubjective>unknown</pronounSubjective>\n        <pronounObjective>unknown</pronounObjective>\n        <pronounPosessive>unknown</pronounPosessive>\n        <seenIntro>false</seenIntro>\n        <eros>false</eros>\n    </profile>\n</data>\n");
            }
            //if set asked for doesn't exist
            else
            {
                Console.WriteLine("unknown string was called");
                return("error");
            }
        }
        //data for string array returning (for long string arrays)
        public static string[] stringArrayReturn(int arrayToReturn)
        {
            //set all the arrays to their proper values
            string [] unknownArray = {"error", "unknown array"}; //-1
            string[] illegalFilenameStrings = {"error", "petsim.", "init", "template", ".vscode", ".cs", ".gitignore", "LICENSE", "favicon.ico", "README.md", "iconSmall.ico", "iconLarge.png"}; //0
            string[] mainMenuArray = {"petsim menu", "1. start gui", "2. start cli", "3. quit"}; //1
            string[] CLImenuArray = {"petsim command line interface", "1. load save file", "2. create save file", "3. delete save file", "4. list files in working directory", "5. go back to previous menu"};//2
            string[] otherMenuArray = {"one", "two"}; //3
            //array returns
            if (arrayToReturn == 0)
            {
                return illegalFilenameStrings;
            }
            if (arrayToReturn == 1)
            {
                return mainMenuArray;
            }
            if (arrayToReturn == 2)
            {
                return CLImenuArray;
            }
            //if array asked for doesn't exist
            else
            {
                Console.WriteLine("unknown array was called");
                return unknownArray;
            }
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
            it assumes element 0 and 1 are always used.
            try to fix this later since this is a very very very very very very very very very very BAD IDEA!!!!!!!!!!!!
            can't be fucked at the moment though... (famous last words)
            tbh the check is simple, just check the number of elements that are not null...
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
            for (int i = 1; i < linesForMenu.Count(); i++) // for (int i = 1; i <= (linesForMenu.Count()-1); i++) // my incompetence knows no bounds
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
        public static string fileNameInputGetter(bool existing, bool deletionSake)
        {
            //create bool to keep track of selection status
            bool selected = false;
            //create variable to keep track of the number of times we've been through the protection loop
            //if the file spposedly already exists (for file editing)
            if (existing == true)
            {
                while (!selected)
                {
                    //ask for filename
                    Console.Write("\nType the name of the existing save file and press ENTER: ");
                    //grab the user's idea of a filename
                    string filenameToEdit = ConsoleInputGrabbingLow.stringInputGetter(true, true);
                    //if we are deleting a file 
                    if(deletionSake)
                    {
                        //make sure the user knows this
                        Console.WriteLine("\nThis will permanently delete the file, are you sure?");
                        //see what the user says
                        bool surely = ConsoleInputGrabbingLow.answerHandlerYesOrNo();
                        //if the user says no
                        if(!(surely))
                        {
                            //return the user changed their mind
                            selected = true;
                        }
                    }
                    //check to make sure it exists
                    if (File.Exists(filenameToEdit))
                    {
                        //return the filename
                        return filenameToEdit;
                    }
                    //otherwise
                    else
                    {
                        //tell the user that the file doesn't exist
                        Console.WriteLine("File doesn't exist.");
                        //exit the loop
                        selected = true;
                    }
                }
            }
            //if the file supposedly doesn't exist (for file creation)
            else if (existing == false)
            {
                while (!selected)
                {
                    //ask for filename
                    Console.Write("\nType a name for the new save file and press ENTER: ");
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
                        if (petsimGeneralTools.DataProcessingToolsLow.illegalStringChecker(filenameToCreate, 0, false))
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
            //eited loop, return error
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
                Console.Write("Type the number of the option you wish to select, then press ENTER: ");
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
    //class for functions used before running the GUI (used as preparations)
    public class PreGraphicalOperations
    {
        //starts the graphical processing
        public static void graphicalStartup()
        {
            //check if the we're running on a desktop OS
            bool currentOS = desktopOperatingSystemChecker();
            //if non-desktop
            if (!(currentOS))
            {
                //start Xamarin runtime
                petsimXamarinRuntime.petsimXamarinStartup();
            }
            //if desktop
            else
            {
                //start GTK runtime
                petsimGTKruntime.petsimGTKstartup();
            }
        }
        //desktop OS checker (returns true if running a desktop OS)
        public static bool desktopOperatingSystemChecker()
        {
            //check for Linux
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return true;
            }
            //check for Windows
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }
            //check for BSD
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                return true;
            }
            //check for MacOS
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return true;
            }
            //if running on none of those
            else
            {
                //return non-desktop (probably mobile)
                return false;
            }
        }
    }
    //class for graphical tools usable by both GTK and Xamarin
    public class petsimIndependentTools
    {
        //
    }
    //class for tools relating to the use of GTK (for desktop users)
    public class petsimGTKtools
    {
        //
    }
    //class for the runtime relating to the use of GTK (for desktop users)
    public class petsimGTKruntime
    {
        //GTK GUI window startup method
        public static void petsimGTKstartup()
        {
            //TODO: expand on this GTK form to make it more useful
            //initialization
            Application.Init();
            //set window properties
            Window mainWindow = new Window("petsim2");
            mainWindow.Resize(640,480);
            //set a label
            Label myLabel = new Label();
            myLabel.Text = "GUI is unavailable.";
            //Add the label to the form
            mainWindow.Add(myLabel);
            //show what we have created
            mainWindow.ShowAll();
            //run the application
            Application.Run();
            //when the form exits
            return;
        }
    }
    //class for handlers of GTK events (for desktop users)
    public class petsimGTKeventHandlers
    {
        //
    }
    /*
    Everything below here is Xamarin, I made a template to get started but I don't know shit about Xamarin.
    I also can't install stuff or emulate stuff just yet so I really can't check your work until release starts.
    Xamarin programmer: you have free reign to do what you want below here as long as it doesn't violate users' rights.
    You can use the GTK appearance to see how the desktop version looks if you want to.
    Our primary target is Android, iOS is secondary but still important, all other mobile OSes can be ignored entirely.
    Please try to document your code, I know I comment a little more than necessary so you don't have to do it as much as I do,
    but please don't write entire methods without commenting what they do.
    Disk space is not an issue, you can use as much of it as you like for the time being.
    Unfortunetely "using Xamarin.Forms;" interferes with "using Gtk;" so I added "using Xamarin;" instead.
    You will have to put "Forms." before all "Xamarin.Forms" calls.
    If you know another way around this, let me know at some point on discord.
    */
    //class for tools relating to the use of Xamarin (for mobile users)
    public class petsimXamarinTools
    {
        //
    }
    //class for the runtime relating to the use of Xamarin (for mobile users)
    public class petsimXamarinRuntime
    {
        //Xamarin GUI screen startup method
        public static void petsimXamarinStartup()
        {
            //TODO: find someone to make a Xamarin GUI for me
            //
            //Xamarin programmer: put Xamarin startup stuff in this method here
            //
            //when the form exits
            return;
        }
    }
    //class for handlers of Xamarin events (for mobile users)
    public class petsimXamarinEventHandlers
    {
        //
    }
}


