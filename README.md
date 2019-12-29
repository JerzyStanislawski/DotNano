<img src="https://avatars2.githubusercontent.com/u/34106716?s=200&v=4" height="150" width="150">

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

### Donate
<a href="nano:nano_3rihs8dcjne3tjummjeotsz8r5aj319huj8xfm6rj16pfyjjri3duzxhionx"><img src="https://user-images.githubusercontent.com/49572068/71561816-fbad3d00-2a7b-11ea-9717-8115fa81e856.png"  height="100" width="100">
</a>
nano_3rihs8dcjne3tjummjeotsz8r5aj319huj8xfm6rj16pfyjjri3duzxhionx
