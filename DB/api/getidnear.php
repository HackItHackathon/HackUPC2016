<?php

class user {
	public $distance;
	public $id;
	public $nom;
	public $latitude;
	public $longitude;
	public $punt;
}

function calcdistance ($la1,$lo1,$la2,$lo2){
	$R = 6371000; // metres
	$ila = ($la2-$la1);
	$ilo = ($lo2-$lo1);
	$a = sin($ila/2) * sin($ila/2) + cos($la1) * cos($la2) * sin($ilo/2) * sin($ilo/2);
	$c = 2 * atan2(sqrt($a), sqrt(1-$a));
	$d = $R * $c;
	return $d;
}

$servername = "localhost";
$username = "hackathon";
$password = "hackit";
$database = "hackathon";

// Create connection
$conn = new mysqli($servername, $username, $password, $database);

// Check connection
if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully";

$sql = "SELECT id, nom, latitude,longitude, punt FROM users WHERE id = '" . $_GET['id'] . "'";

//echo $sql;

$result = $conn->query($sql);

$row= $result->fetch_row();

$user = new user();
$user->distance = 0;
$user->id = $row['id'];
$user->nom = $row['nom'];
$user->latitude = $row['latitude'];
$user->longitude = $row['longitude'];
$user->punt = $row['punt'];

$arrayusers = array();

$sql = "SELECT id, nom, latitude,longitude, punt FROM users WHERE id != '" 
		. $_GET['id'] . "'" . " and ida != '" . $_GET['id'] . "'";

//echo $sql;

$result = $conn->query($sql);

while($row = $result->fetch_assoc()) {
	$userf = new user();
	$userf->id = $row['id'];
	$userf->nom = $row['nom'];
	$userf->latitude = $row['latitude'];
	$userf->longitude = $row['longitude'];
	$userf->punt = $row['punt'];
	$userf->distance = calcdistance($user->latitude, $user->longitude, $userf->latitude, $userf-> longitude);
	array_push($arrayusers, $userf);
}
asort($arrayusers);

echo json_encode($arrayusers);

?>