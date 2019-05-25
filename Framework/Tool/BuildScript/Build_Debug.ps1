#!/bin/bash

cd /d %~dp0

dotnet build --configuration Debug --no-dependencies --no-incremental

read -rsp $'Press any key to continue...\n' -n1 key

exit 0

