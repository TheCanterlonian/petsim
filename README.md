# petsim
petsim is a tamagotchi-like game i am developing. It is a fairly large project but i am confident that it will be completed eventually. It is the most organized project i have ever created. When it has been completed, i will move on to the Metal Labs Proof-of-Concept Game using petsim functionality as a base to build off of. The working files have been labeled "petsim2" because there was a setback in the early development that initiated a full rewrite from scratch.
# Arguments
petsim takes arguments in the following format:
<br>
<br>
petsim2 [c] [g]
<br>
c - start the Command Line Interface instantly
<br>
g - start the Graphical User Interface instantly
<br>
# Filesystem Usage
petsim uses the filesystem to save and load profiles and game state in the directory it is working in. There are a few files that are used by default, including:
<br>
<br>
iconLarge.png - the large icon used by the GUI
<br>
iconSmall.ico - the small icon used by the GUI
<br>
init.xml - contains startup values for petsim
<br>
LICENSE - contains the license for petsim
<br>
README.md - contains usage instructions (this file)
<br>
template.xml - contains default values for creating new save files
<br>
# Compiling
This program can be compiled on your machine so that you can run it on any machine that runs dotnet or mono.
<br>
To run it using dotnet: https://dotnet.microsoft.com/download
<br>
To run it using mono: https://www.mono-project.com/download/stable/
<br>
If you have trouble figuring out what to do, there is a visual walkthrough here: https://github.com/TheCanterlonian/petsim/tree/master/guide
<br>
<br>
Personally, i use both dotnet and mono on multiple distributions of gnu linux, however i can not currently test it on any other Operating System at the moment. If you have tried to compile and run it on another Operating System, please let me know if it works or not.
<br>
<br>
