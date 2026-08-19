import { readFileSync, writeFileSync } from "node:fs";

const [, , changelogPath, version, outputPath] = process.argv;

if (!changelogPath || !version || !outputPath) {
  throw new Error(
    "Usage: node extract-release-notes.mjs <changelog> <version> <output>",
  );
}

const lines = readFileSync(changelogPath, "utf8").split(/\r?\n/);
const headingPrefix = `## [${version}]`;
const startIndex = lines.findIndex((line) => line.startsWith(headingPrefix));

if (startIndex < 0) {
  throw new Error(`Could not find ${headingPrefix} in ${changelogPath}.`);
}

const nextVersionOffset = lines
  .slice(startIndex + 1)
  .findIndex((line) => line.startsWith("## ["));
const endIndex = nextVersionOffset < 0
  ? lines.length
  : startIndex + 1 + nextVersionOffset;
const notes = lines.slice(startIndex + 1, endIndex).join("\n").trim();

if (!notes) {
  throw new Error(`Release notes for ${version} are empty.`);
}

writeFileSync(outputPath, `${notes}\n`, "utf8");
