param
(
	[switch]$build = $false
)

if($build)
{	
	$start = $(get-date)
	docker-compose -f ../compose/docker-compose.yml build
	$endTime = $(get-date) - $start
	$total = "{0:HH:mm:ss}" -f ([datetime]$endTime.Ticks)
	write-host "Build time: $($total)"
	docker-compose -f ../compose/docker-compose.yml up
}

docker-compose -f ../compose/docker-compose.yml up