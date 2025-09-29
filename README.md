# Avro MCP Conventional Commit Server

An MCP (Model Context Protocol) server for assisting with conventional commit message generation and validation, built with clean architecture in C#.

## Features

- Validate conventional commit messages
- Generate conventional commit messages
- Parse commit messages into components
- Retrieve recent commit messages from git repository
- **NEW:** Smart commit generation based on current git changes

## Tools Available

- `validate_commit` - Validates a commit message against conventional commit standards
- `generate_commit` - Generates a conventional commit message with provided parameters
- `parse_commit` - Parses a conventional commit message into its components
- `get_recent_commits` - Retrieves recent commit messages from the git repository
- `generate_smart_commit` - **NEW:** Analyzes current git changes and generates an appropriate commit message

## 📋 Usage Examples

**Приклад 1 - Валідація:**
```
User: "перевір цей коміт: feat: add login form"
AI: [використає validate_commit] "Commit message is valid."
```

**Приклад 2 - Генерація:**
```
User: "створи коміт для виправлення бага в базі даних"
AI: [використає generate_commit] "fix(db): resolve database connection issue"
```

**Приклад 3 - Розумна генерація (NEW):**
```
User: "згенеруй коміт на основі поточних змін"
AI: [використає generate_smart_commit] "feat: add smart commit generation with git analysis

Generated-by: avro-mcp-conv-commit"
```

**Приклад 4 - Генерація з атрибуцією:**
```
User: "створи коміт типу feat з описом 'add user login' та додай атрибуцію"
AI: [використає generate_commit з includeAttribution=true] "feat: add user login

Generated-by: avro-mcp-conv-commit"
```

**Приклад 5 - Отримання історії:**
```
User: "які останні коміти були?"
AI: [використає get_recent_commits] 
"1. feat: add user authentication
2. fix: resolve login bug  
3. docs: update README"
```

## 🔍 Ідентифікація джерела комітів

Коміти, згенеровані MCP сервером, містять спеціальний footer для прозорості:

```
Generated-by: avro-mcp-conv-commit
```

Це дозволяє відрізнити:
- **Ручні коміти** - написані розробником вручну
- **AI-генеровані коміти** - створені AI assistant'ом через MCP
- **MCP-генеровані коміти** - автоматично згенеровані сервером з атрибуцією## Installation

1. Clone this repository
2. Build the project:
   ```bash
   dotnet build
   ```

## Usage in VSCode

1. Build the project in Release mode:
   ```bash
   dotnet publish src/AvroMcpConvCommit.Presentation/AvroMcpConvCommit.Presentation.csproj -c Release
   ```

2. Configure VSCode MCP settings. Add to your VSCode settings (or Cursor settings):

   ```json
   {
     "mcp": {
       "servers": {
         "avro-mcp-conv-commit": {
           "command": "dotnet",
           "args": [
             "C:\\path\\to\\AvroMcpConvCommit.Presentation.dll"
           ]
         }
       }
     }
   }
   ```

   Replace `C:\\path\\to\\AvroMcpConvCommit.Presentation.dll` with the actual path to the published DLL.

## Alternative: Install as NuGet Tool

1. Pack the project:
   ```bash
   dotnet pack src/AvroMcpConvCommit.Presentation/AvroMcpConvCommit.Presentation.csproj -o nupkg
   ```

2. Install globally:
   ```bash
   dotnet tool install --global AvroMcpConvCommit --version 1.0.0
   ```

3. Configure VSCode:
   ```json
   {
     "mcp": {
       "servers": {
         "avro-mcp-conv-commit": {
           "command": "avro-mcp-conv-commit"
         }
       }
     }
   }
   ```

## Architecture

This project follows clean architecture principles:

- **Domain**: Core business entities and logic
- **Application**: Use cases and interfaces
- **Infrastructure**: External dependencies (git integration)
- **Presentation**: MCP server implementation

Each layer is in a separate project, and each class/interface/enum is in its own file.