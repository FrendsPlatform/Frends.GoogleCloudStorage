# Changelog

## [2.0.0] - 2026-10-15
### Changed
- Upgraded the Task to target .NET 8.
- The Task now has an Options parameter. Errors can be thrown or returned in the result with `ThrowErrorOnFailure`, and the message can be customized with `ErrorMessageOnFailure`.
- The result now contains an Error property with details when the operation fails and errors are not thrown.
- Fixed documentation examples of the result.

## [1.0.0] - 2023-05-30
### Added
- Initial implementation
