🎵 Song Catalog CLI
📌 Overview

Song Catalog CLI is a console-based application built in C# that allows users to manage a catalog of songs.
Each song contains a title, artist, and rating (1–5). The app supports adding, removing, searching, sorting, merging catalogs, and undo/redo functionality with persistent storage.

▶️ How to Run
Run Published Version (Recommended)
Go to the Releases section of this repository
Download the latest .zip release
Extract the files
Run:
SongCatalog.exe

No installation or .NET SDK required if the correct runtime is included in the publish.

🧾 How to Use

All commands use | as a separator:

add|Numb|Linkin Park|5
list
remove|Numb|Linkin Park
search|linkin
change rating|Numb|Linkin Park|4
merge
merge external|C:\path\file.json
undo
redo
exit

💡 Features
Add / remove songs
Search by title or artist (case-insensitive, partial match)
Sort by artist, title, or rating
Merge friend or external catalogs
Undo / redo across sessions (persistent)
JSON-based storage system

⚙️ Assumptions
Commands must use |
Ratings are between 1–5
Song uniqueness = title + artist
JSON files are created automatically if missing
External file paths must be valid and accessible

📦 Release Notes

The application is published as a standalone executable.
Users can download and run it without Visual Studio or the .NET SDK.

🤖 AI Usage Note

AI tools were used to assist with designing the undo/redo system and improving the overall architecture of the CLI and repository structure. 
They also helped draft and refine the README and the in-application help message. 
All implementation work, debugging, and final design decisions were completed manually. 
One notable decision where I overrode an AI suggestion was the application architecture for command handling: 
Instead of implementing each command as a separate class, I chose to keep all command logic as methods within the service layer for simplicity and easier maintainability in the scope of this project.