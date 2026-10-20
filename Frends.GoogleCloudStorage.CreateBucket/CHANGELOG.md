# Changelog

## [2.0.0] - 2026-10-15

### Added

- Added error handling with cancellation support.
- Errors can be thrown or returned in the result with `ThrowErrorOnFailure`, and the message can be customized with `ErrorMessageOnFailure`. Both parameters are added in new `Options` tab.
- **[Breaking Change]** Added a credential source selector for JSON credentials or a credentials file.

### Changed

- Updated the target framework to .NET 8.
- [Breaking Change] Validation failures now raise validation exceptions before the operation starts.
- The result includes error details when the operation fails and errors are not thrown.

## [1.0.0] - 2023-05-30
### Added
- Initial implementation
