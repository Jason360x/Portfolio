# Portfolio
This is my personal portfolio of projects.

## Study Projects
These projects were created as part of a module or class of my course.

### Sport-Challenge-Project
<p align="center">
  <img src="/_screenshots/scp.png" alt="Screenshot of the frontpage of the Sport-Challenge-Project">
</p>

The Sport-Challenge-Project was conducted as part of our fourth semester class "Projekt Softwaretechnik". It was created for and under the guidance of doubleSlash Net-Business GmbH. We were tasked with creating a web plattform for their internal "Sport-Challenge" program where employees can group up in teams and are rewarded for being active.

Our platform uses a three tier architecture. It authenticates users via SAML and allows them to create and manage Challenges, Sports and timed Bonuses, as well as documenting their Activities for a specific challenge. The app uses no form of authorization, thus any user is permitted to do any action.

It uses the following underlying technologies:

- React (Frontend)
- Spring Boot (Backend)
- PostgreSQL (Backend Database)
- Keycloak (SAML Provider - can be swapped out with any other correctly configured SAML provider)

The team consisted of the following members:
- Jason Patrick Duffy
- Robin Hackh
- Tom Nguyen Dinh
- Mason Schönherr

I served as Full-Stack-Developer, Scrum-Master and Team- Backend- and DB-Lead. My responsibilites were to manage the team and the project as a whole, as well as making sure our defined rules were followed correctly.

For more information on this project, please take a look at the [repository](https://github.com/JasonDuffy/Sport-Challenge-Project) and the included documentation (German).

### Library Project
<p align="center">
  <img src="/_screenshots/library.png" alt="Screenshot of the frontpage of the library project">
</p>

The library project was part of the Software Engineering module of the third semester of my course.
It was an agile project using Scrum and was written in Java with the GUI components being created using JavaFX.

The program supports logging in with a user and, depending on the user's privileges, add and remove books from the library and/or borrow and return books.
It was created to be a management software for a fictitious library.

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
  <img src="/_screenshots/library_return.png" alt="Screenshot of the return page of the library project">
</p>

I:
- ...created the GUI and logic for the return of the books.
- ...helped create the DB table for the list of borrowed books and conceptionalized the return logic.
- ...created the rating system with a DB table.
- ...helped other team members with various problems.

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

The flight radar was created as part of the lab for the Object Oriented Systems 2 module of the third semester of my course.

It fetches real time plane data from an online API (https://opensky-network.org/api/states/all) and displays each received plane on a map and in a list.
It is written in Java, the GUI elements are created using JavaFX.

We were given a partially started project to work on, so not every piece of code was written by me and we were also given a custom implementation of the LeafletMap.
However, most was written by me and the authors of the file are stated at the beginning of each file. If there is nothing stated, the file was written by someone else.

Note: The final project uses the "Acamo.java" file in "FlightRadar\src\acamo" as its main file.

### Minesweeper
<p align="center">
  <img src="/_screenshots/minesweeper.png" alt="Screenshot of Minesweeper">
</p>

This Minesweeper game was created as part of the lab for the Internet Technology module of the third semester of my course.

It uses JavaScript to build the page and the game logic and CSS to style the page.

The game can run in 2 modes:
- Local
- Server

This can be set by changing the script in line 15 to "localLogic" or "remoteLogic" respectively.

Server logic communicates with a remote server set up by the lecturer to define a playing field and decide which fields to open, etc.

As a "mobile first" application, it is designed primarily for mobile screens but also works perfectly on desktop computers.

## Personal Projects
These projects were created out of personal interest.

### Jason's RPG Tool
<p align="center">
  <img src="/_screenshots/rpgtool.png" alt="Screenshot of the Asterius RPG mode of the RPG Tool">
</p>

There are various tools for many of the mainstream tabletop RPGs like Dungeons and Dragons available.
However, when going with a homebrew approach, a custom tool is needed if you want everything to be in one place.
My RPG Tool is specifically set up for two different homebrew RPGs me and a friend of mine created.

With over 9000 executable lines of code, the project is fairly big.

**IMPORTANT:** Each line of this code was created while I was first learning to program back in 2019 and as such, much of the code is very messy and ignores many programming and OOP principles and best practices. Even then, I think it is still useful as a showcase of a fully working program I created over a longer period of time. Just don't take this as an indicator for my current abilities or work style.

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
  <img src="/_screenshots/snake.png" alt="Screenshot of Snake">
</p>

This Snake game was created over the course of a couple of hours as a way of our IT teacher keeping the advanced students occupied while he went over the basics again with the less advanced students.

Our only task was to try and create a Snake game in any way we wanted.

I decided to use the Unity Engine as the base for the game and C# as the programming language.

Every piece of code and every asset (save for the Snake logo) was created by me.

The game is designed to always be run at an equal aspect ratio (square). Other ratios work but look wrong. Keep that in mind when trying out the game. The build files are set up to always open in a square window.
