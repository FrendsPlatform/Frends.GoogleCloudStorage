# Changelog

## [1.1.0] - 2025-06-18
### Removed
- Eliminated the `ListBuckets` call used only for error handling. The bucket name is now passed directly to `FindMatchingFiles`.

### Fixed
- Access-policy failure when the `storage.buckets.list` permission is missing.

## [1.0.0] - 2023-06-12
### Added
- Initial implementation
