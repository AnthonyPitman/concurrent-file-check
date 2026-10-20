# concurrent-file-check

Test program to experiment with a small concurrency demo.

## Prompt

Each week, deliver a short project-based C# practice session for an early-career developer with a solid computer science foundation. Aim for 30–45 minutes and keep the session concise (about 250–400 words). Include a week number, topic and project title; one concrete coding task with clear inputs, outputs, constraints and 2–3 acceptance criteria; a brief explanation of the underlying concept; and one stretch question. Rotate in this order: concurrency (asynchronous I/O and task coordination), multithreading (CPU work and shared state), networking, testing, and performance. Start with approachable fundamentals and increase difficulty gradually on each five-week cycle, revisiting earlier skills and adding one meaningful challenge at a time. Use the previous sessions and any user feedback in this chat to track progression; do not assume that assigned work has been completed. Prefer small console projects and standard C#/.NET libraries, and specify any necessary dependencies. Give exercises and hints without supplying a full solution unless requested. Deliver a new session and notify the user on every scheduled weekly run.

## Output

### Week 1 Concurrency: Build an asynchronous file summarizer

**Time**: 30-45 minutes
**Dependencies**: .NET SDK; no external packages.

Build a console app that summarizes several text files while allowing their reads to overlap.

**Coding task**

Accept file paths as command-line arguments. For each file, asynchronously read its contents and return a result containing its path, word count, and any errors. Treat a word as a sequence of characters separated by whitespace.

Print one result per file in the original argument order, followed by the total word count for successfully read files. For examples:

```text
notes.txt 120 words
missing.txt ERROR - file not found
ideas.txt: 85 words
Total: 205 words
```

Use `File.ReadAllTextAsync`, `async`/`await`, and `Task.WhenAll`. Start all reads before awaiting their combined completion. Avoid `.Result`, `.Wait()`, and `Task.Run` for file reading. Keep results local to each operation rather than updating a shared total.

For this first exercise, assume a small number of files that comfortably fit in memory. Handle expected file-access failures inside each file's operation so one failed read does not prevent other results from appearing.

**Acceptance criteria**

- Two valid files produce correct individual counts and the correct total; an empty file counts as zero.
- A missing file produces an error result while valid files still succeed.
- Reads can overlap, but printed results preserve input order.

**Underlying concept**

Concurrency lets multiple operations make progress during overlapping periods. Asynchronous I/O allows your program to await file reads without keeping a calling thread blocked throughout the wait; it does not imply a dedicated thread per file. `Task.WhenAll` coordinates completion and returns results in the order of its input tasks.

**Hint**: Create a helper that returns one file's result. Build a task array from the paths, await it once, then calculate the total.

**Stretch question**: If the input grows to 10,000 large files, what could go wrong with starting every read immediately, and how would you limit concurrent reads while preserving output order?
