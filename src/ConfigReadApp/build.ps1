docker build -t iezious/config-tests --network="host" .
docker push iezious/config-tests

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');