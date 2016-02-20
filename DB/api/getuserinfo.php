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


$arr = array('id' => $row[0], 'nom' => $row[1],'punt' => $row[5],
		'location' => array('latitude' => $row[2], 'longitude' => $row[3]), 'ida' => $row[4]);

echo json_encode($arr);

?>