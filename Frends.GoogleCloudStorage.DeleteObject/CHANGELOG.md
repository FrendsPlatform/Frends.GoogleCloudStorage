# Changelog

## [2.0.0] - 2026-10-15
### Added
- Added error handling with cancellation support.
- **[Breaking Change]** Added `Options` tab with `ThrowErrorOnFailure` and `ErrorMessageOnFailure` parameters for flexible error reporting.
- **[Breaking Change]** Added Input parameter `CredentialsType` to define credentials source.

### Changed
- Update target framework to .NET 8.
- [Breaking Change] Exceptions raised regarding validation parameters are changed.
- [Breaking Change] The Task returns a single `Result` object with `Success`, `Objects` and `Error` properties. The list of deleted objects is available in `Result.Objects`.

## [1.2.0] - 2025-10-17
### Fixed
- Fix thrown exception when deleting an object from a non-existing bucket

## [1.1.0] - 2025-10-15
### Changed
- Simplify check for bucket to use the least set of needed permissions

## [1.0.0] - 2023-07-24
### Added
- Initial implementation
