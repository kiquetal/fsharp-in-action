# Running F# Code and Managing Chapter Files

## 1. Building and Running the Project
This project uses the .NET CLI. Here are the primary commands:

### Build the Project
To compile the project and check for errors without running it:
```bash
dotnet build
```

### Run the Project
To build and execute the project:
```bash
dotnet run
```
Since this project uses command-line arguments to select chapters, you can pass them after `--`:
```bash
dotnet run -- 2
```

### Clean the Project
To remove build artifacts (bin and obj folders):
```bash
dotnet clean
```

## 2. The Importance of File Order
In F#, **compilation order matters**. The compiler processes files from top to bottom.
- A file can only use code from files that appear **above** it in the `<ItemGroup>` list in `fsharp-in-action.fsproj`.
- The file containing the `EntryPoint` (or top-level executable code) should typically be the **last** file in the list.

**Current Observation:**
In your `fsharp-in-action.fsproj`, `ChapterTwo.fs` is correctly listed **before** `Program.fs`, allowing `Program.fs` to access the `ChapterTwo` module.

## 3. Options for Creating and Running Files for Every Chapter

Here are the best strategies to manage code for multiple chapters:

### Option A: Modules + Central Entry Point (Recommended)
Create a separate file for each chapter defining a `module`. Call the specific chapter's logic from `Program.fs`.

1.  **Create the file** (e.g., `ChapterTwo.fs`) and define a module:
    ```fsharp
    // ChapterTwo.fs
    module ChapterTwo

    let run () =
        printfn "Running Chapter 2 Examples..."
    ```

2.  **Update `.fsproj`** to ensure the chapter file is **above** `Program.fs`:
    ```xml
    <ItemGroup>
        <Compile Include="ChapterTwo.fs" />
        <Compile Include="Program.fs" />
    </ItemGroup>
    ```

3.  **Call it in `Program.fs`**:
    ```fsharp
    // Program.fs
    [<EntryPoint>]
    let main args =
        ChapterTwo.run() // Switch this call to run different chapters
        0
    ```

### Option B: Command Line Arguments
Modify `Program.fs` to accept arguments determining which chapter to run. This lets you switch without changing code.

```fsharp
// Program.fs
[<EntryPoint>]
let main args =
    match args with
    | [| "2" |] -> ChapterTwo.run()
    | [| "3" |] -> ChapterThree.run() // Assuming you created this
    | _ -> printfn "Please specify a chapter number (e.g., dotnet run -- 2)"
    0
```
Run with: `dotnet run -- 2`

### Option C: F# Scripts (.fsx)
For quick experiments that don't need the full build process, you can use F# script files.

1.  Create `ChapterTwo.fsx`.
2.  Run it directly using F# Interactive:
    ```bash
    dotnet fsi ChapterTwo.fsx
    ```
*Note: Scripts are standalone and don't automatically share code with the rest of the project unless loaded explicitly.*
