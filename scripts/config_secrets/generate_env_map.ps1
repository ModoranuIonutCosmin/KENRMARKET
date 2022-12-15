

Function GetKeyVaultEntries(
    [string]$keyVaultName,
    [string]$destination,
    [string]$separator = ":"
)
{
    $keyVaultEntries = (az keyvault secret list --vault-name $keyVaultName | ConvertFrom-Json) | Select-Object id, name
    
    foreach($entry in $keyVaultEntries)
    {
        $secretValue = (az keyvault secret show --id $entry.id | ConvertFrom-Json) | Select-Object name, value
        $env_config = $env_config + "   $($secretValue.name)${separator}""$($secretValue.value)""`n"
    }

    $env_config | Out-File -FilePath $destination
}


echo $args[0]
echo $args[1]

$key_val_separator = $args[2] ?? ":"
GetKeyVaultEntries $args[0] $args[1] $key_val_separator