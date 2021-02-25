# OpenSkyRestClient #

Implements a .NET Client for the OpenSky REST API:

* [https://opensky-network.org/apidoc/rest.html](https://opensky-network.org/apidoc/rest.html)

## Installing ##

To install OpenSkyRestClient, run the following command in the Package Manager Console:

```
PM> Install-Package OpenSkyRestClient
```

## Usage ##

```csharp
// Create a new OpenSkyClient:
var client = new OpenSkyClient();

// And get all StateVectors from the REST API:
var stateVectors = client.GetAllStateVectorsAsync();
```

