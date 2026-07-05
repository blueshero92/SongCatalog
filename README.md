# 🎵 Song Catalog CLI

> A console-based application built in C# for managing a personal music catalog.

Each song holds a **title**, **artist**, and **rating** (1–5). The app supports adding, removing, searching, sorting, merging catalogs, and fully persistent **undo/redo** across sessions.

---

## 📌 Overview

Song Catalog CLI is a lightweight tool that runs entirely from the command line. All data is stored in JSON files, so your catalog and history persist between runs — no database required.

---

## ▶️ How to Run

### Published Version

1. Go to the [Releases](../../releases) section of this repository
2. Download the latest `.zip` release
3. Extract the files
4. Run:

```bash
SongCatalog.exe
```

> No installation or .NET SDK required.

---

## 🧾 How to Use

All commands use `|` as a separator.

| Command | Description |
|---|---|
| `add\|Title\|Artist\|Rating` | Add a new song |
| `list` | List all songs in the catalog |
| `remove\|Title\|Artist` | Remove a song |
| `search\|query` | Search by title or artist |
| `sort artist` | Sort by artist name |
| `sort title` | Sort by title |
| `sort rating` | Sort by rating (descending) |
| `change rating\|Title\|Artist\|NewRating` | Update a song's rating |
| `merge` | Merge the built-in friend catalog |
| `merge external\|C:\path\to\file.json` | Merge an external catalog file |
| `undo` | Undo the last operation |
| `redo` | Redo the last undone operation |
| `exit` | Exit the application |

### Example Session

```
add|Numb|Linkin Park|5
list
search|linkin
change rating|Numb|Linkin Park|4
remove|Numb|Linkin Park
undo
redo
exit
```

---

## 💡 Features

- ➕ Add and remove songs
- 🔍 Search by title or artist *(case-insensitive, partial match)*
- 🔃 Sort by artist, title, or rating
- 🔀 Merge a friend's or external catalog
- ↩️ Undo / redo operations — **persisted across sessions**
- 💾 JSON-based storage system

---

## ⚙️ Assumptions

- Commands must use `|` as separator
- Ratings must be between **1 and 5** and are floating-point numbers for better precision
- Song uniqueness is determined by **title + artist**
- JSON files are created automatically if missing
- External file paths must be valid and accessible

---

## 📦 Release Notes

The application is published as a **standalone executable**.  
Users can download and run it without Visual Studio or the .NET SDK.

---

## 🤖 AI Usage Note

AI tools were used to assist with designing the undo/redo system and improving the overall architecture of the CLI and repository structure. They also helped draft and refine the README and the in-application help message.

All implementation work, debugging, and final design decisions were completed manually.

One notable decision where I overrode an AI suggestion was the application architecture for command handling: instead of implementing each command as a separate class, I chose to keep all command logic as methods within the service layer.