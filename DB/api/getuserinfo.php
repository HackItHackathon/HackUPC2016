<?php

//echo $_GET['id'];

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

$sql = "SELECT id, nom, latitude,longitude,ida,punt FROM users WHERE id = '" . $_GET['id'] . "'";

//echo $sql;

$result = $conn->query($sql);

$row= $result->fetch_row();

$arr = array('id' => $row['id'], 'nom' => $row['nom'],'punt' => $row['punt'],
		'location' => array('latitude' => $row['latitude'], 'longitude' => $row['longitude']), 'ida' => $row['ida']);

echo json_encode($arr);

?>