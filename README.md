# Portfolio
This is my personal portfolio of projects.

## Study Projects
These projects were created as part of a module or class of my course.

### Library Project
<p align="center">
  <img src="/_screenshots/library.png" height="500" alt="Screenshot of the frontpage of the library project">
</p>

The library project was part of the "Softwaretechnik" module in the third semester of my course.
It was an agile project using Scrum and was written in Java with the GUI components being created using JavaFX.

The program supports logging in with a user and, depending on the user's privileges, add and remove books from the library and/or borrow and return books.
It was meant to be a management software for a fictitious library.

Please note that many GUI elements, especially on the main page, only serve decorative functions and have not been implemented because of the nature of the project's scope.

The project was developed by:
- Karol Fedurko
- Daniel Hammerschmidt
- Tim Karelin
- Nicolai Herrmann
- Robin Hackh
- Jason Patrick Duffy

My concrete responsibility was the functionality of the book returns. 

(see "LibraryProject\src\main\java\swt\library\ReturnBookController.java" and "LibraryProject\src\main\resources\swt\library\returnBook.fxml")

<p align="center">
  <img src="/_screenshots/library_return.png" height="500" alt="Screenshot of the return page of the library project">
</p>

I:
- ...created the GUI and logic for the return of the books.
- ...helped create the DB table for the list of borrowed books and conceptionalized the return logic.
- ...created the rating system with a DB table.
- ...helped other teammembers with various problems.

The login details for the admin account are:
- username: admin
- password: test

The details for the user account are:
- username: Max2003
- password: MusterPW

### Flight Radar
<p align="center">
  <img src="/_screenshots/flightradar.png" alt="Screenshot of flight radar in operation">
</p>

The flight radar was created as part of the lab for the "Objektorientierte Systeme 2" module in the third semester of my course.

It fetches real time plane data from an online API (https://opensky-network.org/api/states/all) and displays each received plane on a map and in a list.
It is written in Java, the GUI elements are created using JavaFX.

We were given a partially started project to work on, so not every piece of code was written by me and we were also given a custom implementation of the LeafletMap.
However, most was written by me and the authors of the file are stated at the beginning of each file. If there is nothing stated, the file was written by someone else.

Note: The final project uses the "Acamo.java" file in "FlightRadar\src\acamo" as its main file.

### Minesweeper
<p align="center">
  <img src="/_screenshots/minesweeper.png" height="500" alt="Screenshot of Minesweeper">
</p>

This Minesweeper game was created as part of the lab for the "Internet-Technologien" module in the third semester of my course.

It uses JavaScript to build the page and the game logic and CSS to style the page.

The game can run in 2 modes:
- Local
- Server

This can be set by changing the script in line 15 to localLogic or remoteLogic respectively.

Server logic communicates with a remote server set up by the lecturer to define a playing field and decide which fields to open, etc.

As a "mobile first" application, it is designed primarily for mobile screens but also works perfectly on desktop computers.

## Personal Projects
These projects were created because of personal interest.

### Jason's RPG Tool
<p align="center">
  <img src="/_screenshots/rpgtool.png" height="500" alt="Screenshot of the Asterius RPG mode of the RPG Tool">
</p>

There are various tools for many of the mainstream tabletop RPGs like Dungeons and Dragons available.
However, when going with a homebrew approach, a custom tool is needed when you want everything to be in one place.
My RPG Tool is specifically set up for two different homebrew RPGs me and a friend of mine created.

With over 9000 executable lines of code, the project is fairly big.

Note that every line of code was created while I was first learning to program in 2019 and as such, much of the code is very messy and ignores many programming and OOP principles and best practices. Even then, I think it is still useful as a showcase of a fully working program I created.

The project is written in C# using Visual Studio. The GUI was created using Windows Forms.

Features:
- 2 RPGs implemented with their respective rulesets
- Automatic switch to English language if the current locale is not German
- Creation of save files using XML
- Import and export of save files using .zip
- Shop function that allows the creation of an item table and later display in the respective RPG
- Dice roll function that also allows custom dice

### Snake
<p align="center">
  <img src="/_screenshots/snake.png" height="500" alt="Screenshot of Snake">
</p>

This Snake game was created over the course of a couple of hours as a way of our IT teacher keeping the advanced students occupied while he went over the basics again with the less advanced students.

Our only task was to try and create a Snake game in any way we wanted.

I decided to use the Unity Engine as the base for the game and C# as the programming language.

Every piece of code and every asset (save for the Snake logo) was created by me.

The game is designed to always be run at an equal aspect ratio (square). Other ratios work but look wrong. Keep that in mind when trying out the game. The build files are set up to always open in a square window.