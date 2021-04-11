[![Nuget](https://img.shields.io/nuget/v/MySqlSharp)](https://www.nuget.org/packages/MySqlSharp/)

# MySqlSharp

A dotnet 6 wrapper around the MySQL C library `libmysql.dll`, `libmysqlclient.dylib` or `libmysqlclient.so`

## Supported MySQL version

MySQL Server 5.7+

## Setup

Install MySQL client library first. MySqlSharp doesn't include any binaries.

## Usage

MySqlSharp wrapps the C APIs from `libmysqlclient`. All APIs are almost the same as the original C API.  
See [tests/MySqlClientTests.cs](https://github.com/ryancheung/MySqlSharp/blob/main/tests/MySqlClientTests.cs) for usage examples.

NOTE if you're using MySQL 5.7, you'll have to use the *OLD structs for data marshaling.

## API Reference

https://dev.mysql.com/doc/c-api/8.0/en/mysql-init.html

