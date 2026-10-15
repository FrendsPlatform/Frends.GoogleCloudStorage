# Changelog

## [2.0.0] - 2026-10-15

### Added

- Added error handling with cancellation support.
- Added `Options` parameter to allow for more flexible configuration.
- [Breaking Change] Added Input parameter to define credentials source.

### Changed

- The Task now returns a single result object with `Success`, `Objects` (the list of downloaded objects) and `Error`, instead of a plain list.
- Update target framework to .NET 8.
- [Breaking Change] Exceptions raised regarding validation parameters are changed.
- Added `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options for controlling error handling.

## [1.1.0] - 2025-06-18
### Removed
- Eliminated the `ListBuckets` call used only for error handling. The bucket name is now passed directly to `FindMatchingFiles`.

### Fixed
- Access-policy failure when the `storage.buckets.list` permission is missing.

## [1.0.0] - 2023-06-12
### Added
- Initial implementation
