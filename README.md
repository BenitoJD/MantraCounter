# 🧘 MantraCounter

**MantraCounter** is a minimalist desktop application built with **C#** for tracking mantra recitations.  
It provides a simple and intuitive interface for spiritual practitioners to **count, track, and manage their mantra repetitions** efficiently.

---

## 🧭 About

MantraCounter is designed to simplify your spiritual practice by handling all counting and tracking tasks.  
Key goals include:

- Simple and intuitive counting interface  
- Local storage of daily and total counts using **SQLite**  
- Reminders or notifications for daily practice  
- Minimal setup and resource usage  

It’s ideal for individuals practicing meditation, chanting, or mantra repetition who want to **focus on practice, not tracking**.

---

## ⚙️ Tech Stack

| Layer | Technology | Purpose |
|--------|-------------|----------|
| **Framework** | .NET / Windows Forms or WPF | Desktop application framework |
| **Language** | C# | Backend and UI logic |
| **Database** | SQLite | Local storage for counts and history |
| **UI** | Windows Forms / WPF | User interface for counting and tracking |
| **Logging** | Built-in C# logging | Error tracking and debugging |

---

## 🚀 Features

- Manual or keyboard-based mantra counting  
- Reset daily counts automatically at midnight  
- Store cumulative totals locally  
- Optional notifications or reminders for practice  
- Minimal and distraction-free user interface  

---

## 🛠 Installation & Running

### Prerequisites

- **Windows 10/11**  
- [.NET 8 SDK](https://dotnet.microsoft.com/download) (if building from source)  
- Visual Studio or JetBrains Rider (for building from source)

### Running Prebuilt App

1. Download the latest release `.exe` from the GitHub Releases page  
2. Run `MantraCounter.exe`  
3. Start counting mantras immediately  

### Building from Source

```bash
git clone https://github.com/BenitoJD/MantraCounter.git
cd MantraCounter
# Open the solution in Visual Studio
# Build and run the project
