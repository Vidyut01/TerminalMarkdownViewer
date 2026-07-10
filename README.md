# Terminal Markdown Renderer

This is a .NET CLI tool that renders Markdown files as highlighted, paginated output directly in your terminal. Like `less`, but for Markdown.

## Install

```bash
dotnet pack
dotnet tool install --global --add-source ./nupkg mdrenderer
```

## Usage

```bash
mdrenderer <file.md>
```

## Keybindings

| Key | Action |
|-----|--------|
| `↑` / `k` | Scroll up |
| `↓` / `j` | Scroll down |
| `PgUp` | Page up |
| `PgDn` / `Space` | Page down |
| `Home` | Jump to top |
| `End` | Jump to bottom |
| `q` | Quit |

## Supported Markdown

- Headings (`#`–`######`), with distinct colors per level
- Paragraphs, with inline emphasis (`*italic*`, `**bold**`, `~~strikethrough~~`, `++underline++`)
- Inline code and fenced/indented code blocks
- Ordered and unordered lists (nested)
- Blockquotes (nested)
- Tables (via pipe tables)
- Links, autolinks, and images (rendered as placeholders)
- Thematic breaks (`---`)

Unrecognized block types fall back to rendering the raw source text.

## Built with

- [Markdig](https://github.com/xoofx/markdig) for Markdown parsing
- [Spectre.Console](https://spectreconsole.net/) for terminal rendering
