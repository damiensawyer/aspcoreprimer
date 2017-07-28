## Troubleshooting Notes
- Make sure that you start vs code from the src/firstcore folder - otherwise it won't find the settings file (that I created) and won't debug.
- install packagages with ```dotnet restore```
- build with ```dotnet build```
- run with ```dotnet run```

- API Reference [here](https://docs.microsoft.com/en-us/dotnet/api/?view=netcore-1.1)
- Good CLI reference [here](https://docs.microsoft.com/en-us/dotnet/articles/core/tools/)

- Learn [Unit Testing](https://github.com/dotnet/docs/tree/master/samples/core/getting-started/unit-testing-using-dotnet-test)

- [10 Great tips for porting](https://stackify.com/15-lessons-learned-while-converting-from-asp-net-to-net-core/)

## version update
See this. 
https://github.com/dotnet/cli/issues/4683

I updated from dotnet preview 0.99999999 to v1.0.4 and it broke this project. I suspect due to the project.json obsolencence.