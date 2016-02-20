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
	if(abs($row['latitude'])<3.5 and abs($row['longitude'])<3.5){
		$userf->latitude = $row['latitude'];
		$userf->longitude = $row['longitude'];
		$userf->punt = $row['punt'];
		$userf->distance = calcdistance($user->latitude, $user->longitude, $userf->latitude, $userf-> longitude);
		array_push($arrayusers, $userf);
	}
}
asort($arrayusers);

$num = 1;
$distance = 500000;
if(isset($_GET['num'])) $num = $_GET['num'];
if(isset($_GET['distance'])) $distance = $_GET['distance'];

$i = 0;
$arrayaux = array();
while($i<sizeof($arrayusers) && $i<$num && $arrayusers[$i]->distance <= $distance ){
	array_push($arrayaux, $arrayusers[$i]);
	$i= $i +1;
}
echo json_encode($arrayaux);

?>