

Function AddKeyVaultEntries(
    [string]$keyVaultName,
    [string]$configMapName
)
{

    echo $keyVaultName
    echo $configMapName

    $configmap_creation_command = "kubectl create configmap $($configMapName) "

    $keyVaultEntries = (az keyvault secret list --vault-name $keyVaultName | ConvertFrom-Json) | Select-Object id, name
    
    foreach($entry in $keyVaultEntries)
    {
        $secretValue = (az keyvault secret show --id $entry.id | ConvertFrom-Json) | Select-Object name, value

        $configmap_creation_command += "--from-literal=$($secretValue.name)=""$($secretValue.value)"" "
    }

    echo $configmap_creation_command

    kubectl delete configmap $($configMapName) --ignore-not-found=true

    Invoke-Expression $configmap_creation_command
}

AddKeyVaultEntries $args[0] $args[1]