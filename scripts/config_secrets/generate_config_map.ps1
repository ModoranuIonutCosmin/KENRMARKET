
$header = @"
apiVersion: v1
kind: ConfigMap
metadata:
  name: secrets-connection-strings
data:

"@

./generate_env_map.ps1

$environmentals = Get-Content -Path $args[0]

$header += $environmentals

$header > local-secrets-configmap.yaml

