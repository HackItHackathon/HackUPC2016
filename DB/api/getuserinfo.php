<?php


$arr = array('idu' => 1, 'nom' => "JoanMaria",'punt' => 100,
		'location' => array('latitude' => 23, 'longitude' => 3.1415), 'ida' => 100);

echo json_encode($arr);

?>