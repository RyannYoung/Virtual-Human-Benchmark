# Virtual Human Benchmark

The VR Human Benchmark application (VHB) was developed as a tool to assist research into the benefits of virtual reality on a user's cognitive and physical abilities. The application contains a variety of tests derived from both the Human Benchmark web application, and the BATAK reaction test.

![enter image description here](https://github.com/RyannYoung/VirtualHumanBenchmark/blob/main/Assets/Materials/banner.png?raw=true)

## Description

The project's current state (v1.0) is considered a stable release version that contains three of the four outlined modes within the project scope. These include:

 - **Accumulator Test**: The BATAK lightboard will light up a single target at a time, the user must then strike the target for another to appear. The objective of this mode is to strike as many targets as possible during a set time limit (i.e., 30/60 seconds). The purpose of this challenge is to assess the user’s agility and reaction time.
 - **Reaction Test**: After a random interval the board will activate all targets, the user must then react (by pressing a button on the controller) as fast as possible. This process repeats over a set of intervals (i.e., 5) in which each reaction time is recorded. The purpose of this test is to primarily assess the user’s ability to mentally process information as fast as possible.
 - **Sequence Test**: The board will display a pattern of targets (flashing one after another) which the user must repeat. Every time the user successfully repeats the displayed pattern, the board will then display a pattern with increased difficulty. The purpose of this mode is to examine the short-term memory, cognitive processing, and judgement of the user.

*Note: Additional tests may be developed in the future.* 
## Data Export
Upon completion of a test, a set of data will be generated providing insights into the performance indicators based upon the specific test. These can be found at:
 
**Windows**

    %userprofile%\AppData\LocalLow\Capstone Project\Virtual Human Benchmark

**Android** 

    /storage/emulated/0/Android/Virtual Human Benchmark/files

Under these root directories will be a subfolder for each test (i.e., Reaction Test). Within these folders will be the raw data files for the results of each test (in both json and csv file formats) using the following file naming scheme.

    Capstone Project_<Mode>_<yyyy-dd-mm-hh-mm-ss>.<format>

## Getting Started

### Dependencies
Below are a list of dependencies based on whether the application is run through Unity, Windows Mixed Reality (Windows) or Oculus Runtime (Android)

#### Opening the Project in Unity

 - Unity version 2021.3.0f1 (2021 LTS)
 - Windows 10/11

#### Windows Mixed Reality
- Windows Mixed Reality support device (i.e, HP Reverb G2)
- Windows 10/11

#### Oculus Runtime
- Oculus Runtime supported device (Oculus/Meta Quest 2)

### Installing

* Follow this link to go to the latest release version and follow the instructions on how to install the application

## Authors
Contributor's of the Capstone project:

 - Ryan Young 
 - Liam Searle 
 - Kevin Adamz 
 - Tashi Dhenup

## Version History

* 0.1
    * Initial Release

## License

This project is licensed under the GNU General Public License v3.0 - see the LICENSE.md file for details
