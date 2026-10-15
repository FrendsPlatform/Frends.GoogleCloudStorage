# Changelog

## [2.0.0] - 2026-10-15
### Changed
- Breaking: the Task now requires an `Options` parameter and returns a single result object with `Success`, `Objects` (the list of downloaded objects) and `Error`, instead of a plain list.
- Upgraded the Task to target .NET 8.

### Added
- `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options for controlling error handling.
- Input parameters are validated before the download starts.

## [1.1.0] - 2025-06-18
### Removed
- Eliminated the `ListBuckets` call used only for error handling. The bucket name is now passed directly to `FindMatchingFiles`.

### Fixed
- Access-policy failure when the `storage.buckets.list` permission is missing.

## [1.0.0] - 2023-06-12
### Added
- Initial implementation
