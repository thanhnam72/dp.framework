#!/bin/bash

dotnet msbuild -p:Configuration=Debug

read -rsp $'Press any key to continue...\n' -n1 key

exit 0

