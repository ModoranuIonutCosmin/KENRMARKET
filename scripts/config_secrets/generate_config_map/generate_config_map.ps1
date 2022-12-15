
$header = @"
apiVersion: v1
kind: ConfigMap
metadata:
  name: secrets-connection-strings
data:

"@

$key_vault_name = $args[0] ?? "secretskeyvaultkmarket02"
$file_name = $args[1] ?? "local-secrets.env"

./generate_env_map.ps1 $key_vault_name $file_name ":"

$environmentals = Get-Content -Raw -Path $file_name

$header += $environmentals

$header > local-secrets-configmap.yaml

