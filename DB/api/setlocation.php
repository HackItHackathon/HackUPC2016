<?php

//http://interact.siliconpeople.net/hackathon/setlocation?id=9&latitude=23&longitude=2.332;

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

$sql = "UPDATE users SET latitude='" . $_GET['latitude'] . "', longitude ='" . $_GET['longitude'].
	"' WHERE id = '". $_GET['id'] ."'";

echo $sql;

$result = $conn->query($sql);

?>