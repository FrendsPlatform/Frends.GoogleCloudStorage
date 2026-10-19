# Changelog

## [2.0.0] - 2026-10-15

### Added
- Added error handling with cancellation support.
- **[Breaking Change]** Added the `Options` parameter to allow for more flexible configuration.
- **[Breaking Change]** Added an input parameter to define the credentials source.

### Changed
- Update target framework to .NET 8.
- **[Breaking Change]** Exceptions raised regarding validation parameters are changed.
- Upload Object now returns one task result containing metadata for every uploaded object.

## [1.2.0] - 2025-10-17
### Fixed
- Fix thrown exception when uploading an object to a non-existing bucket

## [1.1.0] - 2025-10-15
### Changed
- Simplify check for bucket to use the least set of needed permissions

## [1.0.0] - 2023-06-08
### Added
- Initial implementation
