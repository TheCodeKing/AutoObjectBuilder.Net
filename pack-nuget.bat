copy LICENSE bin
pushd source\TheCodeKing.ObjectAutoBuilder
copy bin\SignedRelease\AutoObjectBuilder.dll ..\..\bin
..\..\..\..\..\NuGet\NuGet.exe pack TheCodeKing.ObjectAutoBuilder.csproj -Prop Configuration=SignedRelease -o ..\..\bin
popd