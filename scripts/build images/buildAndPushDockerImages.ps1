docker --version

SET DOCKER_BUILDKIT=0
SET COMPOSE_DOCKER_CLI_BUILD=0

$registry = 'modoranuc'

$currentLocation = pwd


Set-Location "../.."

$projectRoot = pwd

echo "Project root is ${projectRoot}"

$dockerfiles = @("${projectRoot}\src\Cart\Cart.API\Dockerfile",
 "${projectRoot}\src\Customers\Customers.API\Dockerfile",
    "${projectRoot}\src\Gateway\Gateway.API\Dockerfile", 
    "${projectRoot}\src\Order\Order.API\Dockerfile", 
    "${projectRoot}\src\Payments\Payments.API\Dockerfile",
    "${projectRoot}\src\Products\Products.API\Dockerfile", 
    "${projectRoot}\src\Watchdog\Watchdog.WebApp\Dockerfile");

foreach ($dockerfile in $dockerfiles) {
    

    $dockerFileParent = Split-Path -Path $dockerfile -Parent

    $name = $dockerFileParent | split-path -leaf
    $name = $name.ToLower().Replace(".", "")

    Set-Location $dockerFileParent

    $tag = "${registry}/${name}:latest"


    echo $tag

    docker build -t $tag -f $dockerfile $projectRoot
    docker push $tag
}

Set-Location $currentLocation