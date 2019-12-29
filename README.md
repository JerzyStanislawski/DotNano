# DotNano
Complete .NET API for Nano RPC protocol.

## Getting Started

### Install

`Install-Package DotNano.RpcApi`

### Usage

```cs
var client = new NanoRpcClient("localhost", 7076);
var response = client.AccountBalance(new PublicAddress("nano_1111111111111111111111111111111111111111111111111117353trpda"));
Console.WriteLine(response.Balance);
```

## Supported RPC methods

All RPC methods are supported.  
They are defined in official [Nano Docs](https://docs.nano.org/commands/rpc-protocol/).

You can regenerate NanoRpcClient class by running DotNano.Executor applciation. The code is generated based on [Nano Docs](https://docs.nano.org/commands/rpc-protocol/) for easy update.

### Naming convention

Method names are camel case of original RPC methods, e.g. account_history => AccountHistory.
