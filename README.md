# Portfolio
This is my personal portfolio of projects.

## Library Project
![Screenshot of the frontpage of the library project](https://github.com/Jason360x/Portfolio/tree/main/_screenshots/library.png)
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

I:
- ...created the GUI and logic for the return of the books
- ...helped create the DB table for the list of borrowed books and conceptionalized the return logic
- ...created the rating system with a DB table
- ...helped other teammembers with various problems

The login details for the admin account are:
- username: admin
- password: test

The details for the user account are:
- username: Max2003
- password: MusterPW

## Jason's RPG Tool
There are various tools for many of the mainstream tabletop RPGs like Dungeons and Dragons available.
However, when going with a homebrew approach, a custom tool is needed when you want everything to be in one place.
My RPG Tool is specifically set up for two different homebrew RPGs me and a friend of mine created.

With over 9000 executable lines of code, the project is fairly big.
Note that every line of code was created while I was first learning to program in 2019 and as such, much of the code is very messy and ignores many programming and OOP principles and best practices. Even then, I think it is still useful as a showcase of a fully working program I created.
The project is written in C# using Visual Studio. The GUI was created using Windows Forms.

Features:
- 2 RPGs implemented with their respective rulesets
- Automatic switch to english language if the current locale is not german
- Creation of save file using XML
- Import and export of save files using .zip
- Shop function that allows the creation of an item table and later display in the respective RPG
- Dice roll function that also allows custom dice

## Flight Radar
The flight radar was created as part of the lab for the "Objektorientierte Systeme 2" module in the third semester of my course.

It fetches real time plane data from an online API (https://opensky-network.org/api/states/all) and displays each received plane on a map and in a list.
It is written in Java, the GUI elements are created using JavaFX.

We were given a partially started project to work on, so not every piece of code was written by me and we were also given a custom implementation of the LeafletMap.
However, most was written by me and the authors of the file are stated at the beginning of each file. If there is nothing stated, the file was written by someone else.

Note: The final project uses the "Acamo.java" file in "FlightRadar\src\acamo" as its main file.

