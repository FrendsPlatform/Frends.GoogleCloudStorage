# Changelog

## [2.0.0] - 2026-10-15

### Added

- Added error handling with cancellation support.
- Added an `Options` parameter for flexible error handling.
- Added a credential source selector for JSON credentials or a credentials file.

### Changed

- Updated the target framework to .NET 8.
- [Breaking Change] Validation failures now raise validation exceptions before the operation starts.
- Errors can be thrown or returned in the result with `ThrowErrorOnFailure`, and the message can be customized with `ErrorMessageOnFailure`.
- The result includes error details when the operation fails and errors are not thrown.

## [1.0.0] - 2023-05-30
### Added
- Initial implementation
