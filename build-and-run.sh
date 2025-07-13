#!/bin/bash
cd /home/petre/repos/InGeorgianLari

# Clean up test files
rm -f TestApi.cs TestProgram.cs test-api.csx 2>/dev/null
rm -rf TestApi/ 2>/dev/null

# Clean build artifacts
echo "Cleaning..."
dotnet clean

# Build
echo "Building..."
dotnet build

# Run if build succeeds
if [ $? -eq 0 ]; then
    echo "Running..."
    dotnet run
else
    echo "Build failed!"
fi