cd repos# Frends.GoogleCloudStorage.CreateBucket

[![Frends.SFTP.DownloadFile Main](https://github.com/FrendsPlatform/Frends.GoogleCloudStorage/actions/workflows/CreateBucket_build_and_test_on_main.yml/badge.svg)](https://github.com/FrendsPlatform/Frends.GoogleCloudStorage/actions/workflows/CreateBucket_build_and_test_on_main.yml)
![MyGet](https://img.shields.io/myget/frends-tasks/v/Frends.SFTP.DownloadFiles?label=NuGet)
![GitHub](https://img.shields.io/github/license/FrendsPlatform/Frends.GoogleCloudStorage?label=License)
![Coverage](https://app-github-custom-badges.azurewebsites.net/Badge?key=FrendsPlatform/Frends.GoogleCloudStorage/Frends.GoogleCloudStorage.CreateBucket|main)

Creates a bucket to Google Cloud Storage.

## Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed

## Building

### Clone a copy of the repo

`git clone https://github.com/FrendsPlatform/Frends.GoogleCloudStorage.git`

### Build the project

`dotnet build`

### Run tests

cd Frends.GoogleCloudStorage.CreateBucket.Tests

Run the Docker compose from Frends.GoogleCloudStorage.CreateBucket.Tests directory using

`docker-compose up -d`

`dotnet test`

### Create a NuGet package

`dotnet pack --configuration Release`
